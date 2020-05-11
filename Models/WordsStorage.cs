using System.Collections.Generic;

namespace MarkovChainTextGeneration.Models {
	public class WordsStorage {
		public Dictionary<string, Word> Words { get; set; } = new Dictionary<string, Word>();
	}
}