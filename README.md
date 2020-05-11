# MarkovChainTextGeneration
Simple text generation based on Markov's chains
Usage
```C#
var textProcessor = new TextProcessor(new WordsStorage()); //Or other WordsStorage instance from database
textProcessor.ProcessTextAsync(someString); //someString is text for generation
var generatedString = textProcessor.GenerateTextAsync();
```
