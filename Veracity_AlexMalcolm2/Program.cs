using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Veracity_AlexMalcolm
{
    public class Program
    {
        public static bool DEBUG_ENABLED = false;

        [STAThread]
        static int Main(string[] args)
        {
            // keep track of runtime
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ConsoleHelper.WriteLine(LogType.PLAIN, "\n\t\tAlex Malcolm - Veracity Code Test 2020\n");

            string inputFile = null;
            bool sortInputs = false;
            bool enableGuiConsumer = false;
            bool throttleInputs = false;
            for (int i = 0; i < args.Length; ++i)
            {
                switch (args[i])
                {
                    case "-input":
                    case "-i":
                    case "input":
                        if (args.Length > i + 1)
                        {
                            inputFile = args[i + 1];
                            ConsoleHelper.WriteLine(LogType.INFO, "Using input: " + inputFile);
                        }
                        else
                            ConsoleHelper.WriteLine(LogType.ERROR, "input flag passed but no accompanying file.");
                        break;

                    case "-debug":
                    case "-d":
                    case "debug":
                        DEBUG_ENABLED = true;
                        ConsoleHelper.WriteLine(LogType.INFO, "verbose debug information enabled");
                        break;

                    case "-gui":
                    case "-g":
                    case "gui":
                        enableGuiConsumer = true;
                        ConsoleHelper.WriteLine(LogType.INFO, "GUI Consumer enabled");
                        break;

                    case "-sort":
                    case "-s":
                    case "sort":
                        sortInputs = true;
                        ConsoleHelper.WriteLine(LogType.INFO, "input sorting enabled, \"File\" will take priority.");
                        break;

                    case "-throttle":
                    case "-t":
                    case "throttle":
                        throttleInputs = true;
                        ConsoleHelper.WriteLine(LogType.INFO, "input throttling enabled, A maximum of 10 will be parsed /sec.");
                        break;

                    case "-help":
                    case "-h":
                    case "help":
                        outputHelp();
                        return 0;
                }
            }

            if (inputFile == null)
            {
                outputHelp();
                return 1;
            }

            if (!File.Exists(inputFile))
            {
                ConsoleHelper.WriteLine(LogType.ERROR, "Input file did not exist or could not be accessed. Check it exists and you have the appropriate permissions.");
                return 1;
            }

            string[] lines = File.ReadAllLines(inputFile);
            string workingDir = Path.GetDirectoryName(inputFile);

            if (lines != null && lines.Length > 0)
            {
                // remove the header from the csv
                //TODO - Alex: check if the first line is actually a header or not
                lines = lines.Skip(1).ToArray();

                Producer producer;
                if (sortInputs)
                    producer = new ProducerPrioritised("File", lines, workingDir, throttleInputs, enableGuiConsumer);
                else
                    producer = new Producer(lines, workingDir, throttleInputs, enableGuiConsumer);

                if (producer != null)
                    producer.Produce();
                else
                    ConsoleHelper.WriteLine(LogType.ERROR, "No valid producer, logical error in code");
            }
            else
                ConsoleHelper.WriteLine(LogType.INFO, "No input detected for producer.");

            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;
            ConsoleHelper.WriteLine(LogType.INFO, "Total runtime - " + string.Format("{0:00}.{1:00}", elapsedTime.Seconds, elapsedTime.Milliseconds / 10));

            Console.ReadKey();
            return 0;
        }

        private static void outputHelp()
        {
            ConsoleHelper.WriteLine(LogType.INFO, "[-help, help, -h] - shows this help.");
            ConsoleHelper.WriteLine(LogType.INFO, "[-debug, debug, -d] - enables verbose debug output during runtime");
            ConsoleHelper.WriteLine(LogType.INFO, "[-gui, gui, -g] - enables GUI consumer");
            ConsoleHelper.WriteLine(LogType.INFO, "[-input, input, -i] {ABSOLUTE_FILE_PATH} - provides the application with the input file. Can be absolute or relative path.");
            ConsoleHelper.WriteLine(LogType.INFO, "[-sort, sort, -s] - enables sorting inputs, prioritising \"File\".");
            ConsoleHelper.WriteLine(LogType.INFO, "[-throttle, throttle, -t] - enables throttling consumption, 10 max per second.");
            ConsoleHelper.WriteLine(LogType.PLAIN);
        }
    }
}
