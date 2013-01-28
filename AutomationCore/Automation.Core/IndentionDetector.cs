using System.Collections.Generic;

namespace Automation.Core
{
    public class IndentionDetector
    {
        private readonly Stack<int> indentPositions;

        private IndentionDetector()
        {
            this.indentPositions = new Stack<int>();
        }

        private int LastPosition
        {
            get { return (this.indentPositions.Count == 0) ? 0 : this.indentPositions.Peek(); }
        }

        public static IndentionDetector GetInstance()
        {
            return new IndentionDetector();
        }

        public IEnumerable<Indention> Detect(int position)
        {
            if (position == this.LastPosition)
            {
                // Position did not change - no change to the indention
                yield break;
            }
            else if (position > this.LastPosition)
            {
                // Position increased - indented block is detected
                this.Push(position);
                yield return Indention.Indent;
            }
            else if (position < this.LastPosition)
            {
                // Position decreased - one or several indented blocks are closed
                while(true)
                {
                    this.Pop();
                    yield return Indention.Dedent;

                    if (position == this.LastPosition)
                    {
                        break;
                    }

                    if (this.LastPosition == 0)
                    {
                        break;
                    }
                }

                // If we exited because we drained stack (last position is 0) and 
                // didn't find corresponding indent, then it is a formatting error
                if (this.LastPosition != position)
                {
                    throw new AutomationException("Input is not well formatted, can't produce correct indention");
                }
            }
        }

        private void Push(int position)
        {
            this.indentPositions.Push(position);
        }

        private void Pop()
        {
            if (this.indentPositions.Count != 0)
            {
                this.indentPositions.Pop();
            }
        }
    }
}
