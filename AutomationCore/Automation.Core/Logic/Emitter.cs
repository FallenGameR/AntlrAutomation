using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace Automation.Core.Logic
{
    public abstract class Emitter
    {
        private readonly Queue<IToken> queuedTokens;

        private Emitter()
        {
            this.queuedTokens = new Queue<IToken>();
        }

        public bool HasTokens
        {
            get { return this.queuedTokens.Any(); }
        }

        public static Emitter GetInstance()
        {
            return new Emitter();
        }

        public void Process(IToken token)
        {
            // Emit tokens if needed
            if (this.IsTrigger(token))
            {
                foreach (var generated in this.Generate(token))
                {
                    this.queuedTokens.Enqueue(generated);
                }
            }

            // Preserve original token
            this.queuedTokens.Enqueue(token);
        }

        public IToken NextToken()
        {
            return this.queuedTokens.Dequeue();
        }

        protected abstract bool IsTrigger(IToken token);

        protected abstract IEnumerable<IToken> Generate(IToken token);
    }
}
