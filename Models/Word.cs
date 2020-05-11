using System.Collections.Generic;

namespace MarkovChainTextGeneration.Models {
	public class Word {
		public string Text { get; set; }
		public int Count { get; set; } = 1;
		public List<Word> NextWords { get; set; } = new List<Word>();
	}
}