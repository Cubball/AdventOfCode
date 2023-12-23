namespace AdventOfCode2023.Day20;

public abstract class CommunicationModule
{
    public CommunicationModule(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public List<CommunicationModule> DestinationModules { get; } = new();

    public abstract void HandleIncomingSignal(CommunicationModule sender, bool signal);

    public abstract bool? GetSignalToSend();
}

public class FlipFlop : CommunicationModule
{
    private bool _signal;
    private bool _receivedSignal;

    public FlipFlop(string name) : base(name) { }

    public override void HandleIncomingSignal(CommunicationModule sender, bool signal)
    {
        if (!signal)
        {
            _signal = !_signal;
            _receivedSignal = true;
        }
    }

    public override bool? GetSignalToSend()
    {
        if (!_receivedSignal)
        {
            return null;
        }

        _receivedSignal = false;
        return _signal;
    }
}

public class ConjunctionModule : CommunicationModule
{
    private readonly Dictionary<CommunicationModule, bool> _lastSignals = new();

    public ConjunctionModule(string name) : base(name) { }

    public void AddInput(CommunicationModule module)
    {
        _lastSignals[module] = false;
    }

    public override void HandleIncomingSignal(CommunicationModule sender, bool signal)
    {
        _lastSignals[sender] = signal;
    }

    public override bool? GetSignalToSend()
    {
        return !_lastSignals.Values.All(x => x);
    }
}

public class BroadcastModule : CommunicationModule
{
    private bool _signal;

    public BroadcastModule(string name) : base(name) { }

    public override void HandleIncomingSignal(CommunicationModule sender, bool signal)
    {
        _signal = signal;
    }

    public override bool? GetSignalToSend()
    {
        return _signal;
    }
}

public class UntypedModule : CommunicationModule
{
    public UntypedModule(string name) : base(name) { }

    public override bool? GetSignalToSend()
    {
        return null;
    }

    public override void HandleIncomingSignal(CommunicationModule sender, bool signal) { }
}

public static class FirstTaskSolution
{
    private static readonly char[] Separators = { ' ', ',' };

    public static int Initial(string[] input)
    {
        var modules = new Dictionary<string, CommunicationModule>();
        var moduleConnections = new Dictionary<string, string[]>();
        foreach (var line in input)
        {
            var spaceIdx = line.IndexOf(' ');
            CommunicationModule module = line[0] switch
            {
                '%' => new FlipFlop(line[1..spaceIdx]),
                '&' => new ConjunctionModule(line[1..spaceIdx]),
                _ => new BroadcastModule("broadcaster"),
            };
            modules[module.Name] = module;
            moduleConnections[module.Name] = line[(spaceIdx + 4)..].Split(Separators, StringSplitOptions.RemoveEmptyEntries);
        }

        foreach (var (moduleName, connectedModules) in moduleConnections)
        {
            var module = modules[moduleName];
            foreach (var connectedModuleName in connectedModules)
            {
                var connectedModule = modules.GetValueOrDefault(connectedModuleName, new UntypedModule(connectedModuleName));
                module.DestinationModules.Add(connectedModule);
                if (connectedModule is ConjunctionModule conjunctionModule)
                {
                    conjunctionModule.AddInput(module);
                }
            }
        }

        var broadcaster = modules["broadcaster"];
        var queue = new Queue<CommunicationModule>();
        var lowPulsesSent = 0;
        var highPulsesSent = 0;
        for (int i = 0; i < 1_000; i++)
        {
            lowPulsesSent++;
            broadcaster.HandleIncomingSignal(null!, false);
            queue.Enqueue(broadcaster);
            while (queue.Count > 0)
            {
                var module = queue.Dequeue();
                var signal = module.GetSignalToSend();
                if (signal is null)
                {
                    continue;
                }

                if (signal.Value)
                {
                    highPulsesSent += module.DestinationModules.Count;
                }
                else
                {
                    lowPulsesSent += module.DestinationModules.Count;
                }

                foreach (var destinationModule in module.DestinationModules)
                {
                    destinationModule.HandleIncomingSignal(module, signal.Value);
                    queue.Enqueue(destinationModule);
                }
            }
        }

        Console.WriteLine($"Low pulses sent: {lowPulsesSent}");
        Console.WriteLine($"High pulses sent: {highPulsesSent}");
        return lowPulsesSent * highPulsesSent;
    }
}