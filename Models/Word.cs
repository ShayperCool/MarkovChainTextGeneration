using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkovChainTextGeneration.Models {
	public class Word {
		public string Text { get; set; }
		public int Count { get; set; }
		public Dictionary<string, Word> NextWords { get; set; } = new Dictionary<string, Word>();

		public Word SelectRandomNextWord() {
			int max = NextWords.Sum(keyValuePair => keyValuePair.Value.Count);

			int targetIndex = new Random().Next(max);
			int current = 0;
			
			foreach (var keyValuePair in NextWords) {
				current += keyValuePair.Value.Count;
				if (current >= targetIndex)
					return keyValuePair.Value;
			}

			return null;
		}
		
	}
}