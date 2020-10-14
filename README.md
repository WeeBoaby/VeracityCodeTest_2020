# Veracity_CodeTest2020_1

## Information:
Created using Visual Studio Community (2019).

| Command						| Description   |
|---							|---|
| [-help, help, -h]				| Shows executable help   |
| [-debug, debug, -d]			| Enables verbose debug output (if any)  |
| [-gui, gui, -g]				| Enables GUI consumer  |
| [-input, input, -i]			| Provides executable with input file in a CSV form. Can be either absolute or relative  |
| [-sort, sort, -s]				| Enables sorting inputs, will prioritise file consumption  |
| [-throttle, throttle, -t]		| Enables input throttling, will throttle to a max of 10 inputs per second  |

Example with every flag enabled: `.\Veracity_AlexMalcolm.exe -i C:\Users\alexm\Documents\CodingTest2020InputStimulusPlusGui.csv -d -t -g -s`

## Requirements:
### Create a simple program in the language of your choice that meets the following requirements:
- [x] Has a single producer and two consumers; file and console
- [x] Producer should create objects that denote a type (i.e. file, console) as well as some string payload i.e. “file”, “I am some data”
- [x] Producer should place created objects into a single generic queue
- [x] Consumers should only process objects in the queue that belong to them (i.e. a file consumer should ignore console type)
- [x] File consumer should create a single file and write all payloads separated with a newline
- [x] Console consumer should write all payloads separated with a newline
- [x] Program should run for ~10 seconds and then shutdown. It should also terminate when told by the user using an appropriate key i.e. escape or ctrl-c
### Additional and optional requirements:
- [x] Add a third consumer that displays the payloads to a webpage, WPF application or some other form of UI.
- [x] Producer should be limited, to at most, 10 objects per second
- [x] The file consumer should always take priority in the queue and have its objects processed first
- [x] Add unit testing support to a single part of the proposed application i.e. tests for a single consumer or producer.
