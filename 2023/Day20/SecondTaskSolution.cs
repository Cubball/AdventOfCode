namespace AdventOfCode2023.Day20;

public static class SecondTaskSolution
{
    private static readonly char[] Separators = new char[] { ',', ' ', '-', '>' };

    public static long Initial(string[] input)
    {
        var modules = new Dictionary<string, CommunicationModule>();
        var moduleConnections = new Dictionary<string, string[]>();
        var inputs = new Dictionary<string, List<string>>();
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
                inputs.TryAdd(connectedModuleName, new List<string>());
                inputs[connectedModuleName].Add(moduleName);
                var connectedModule = modules.GetValueOrDefault(connectedModuleName, new UntypedModule(connectedModuleName));
                module.DestinationModules.Add(connectedModule);
                if (connectedModule is ConjunctionModule conjunctionModule)
                {
                    conjunctionModule.AddInput(module);
                }
            }
        }

        var rxInput = inputs["rx"][0];
        var inputsInputs = inputs[rxInput];
        var cycleLengths = new Dictionary<string, long>();
        var broadcaster = modules["broadcaster"];
        var queue = new Queue<CommunicationModule>();
        var buttonPressesCount = 0;
        while (true)
        {
            buttonPressesCount++;
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

                if (inputsInputs.Contains(module.Name) && signal.Value && !cycleLengths.ContainsKey(module.Name))
                {
                    cycleLengths[module.Name] = buttonPressesCount;
                }

                if (inputsInputs.Count == cycleLengths.Count)
                {
                    return LCM(cycleLengths.Values.ToList());
                }

                foreach (var destinationModule in module.DestinationModules)
                {
                    destinationModule.HandleIncomingSignal(module, signal.Value);
                    queue.Enqueue(destinationModule);
                }
            }

        }
    }

    private static long LCM(List<long> numbers, int index = 0)
    {
        if (index == numbers.Count - 1)
        {
            return numbers[^1];
        }

        var lcm = numbers[index] * numbers[index + 1] / GCD(numbers[index], numbers[index + 1]);
        numbers[index + 1] = lcm;
        return LCM(numbers, index + 1);
    }

    private static long GCD(long a, long b) => a == 0 ? b : GCD(b % a, a);
}