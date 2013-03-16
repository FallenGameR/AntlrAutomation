using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace Automation.Core
{
    public class Emitter
    {
        private readonly IEnumerable<IGenerator> generators;
        private readonly Queue<IToken> queuedTokens;

        private Emitter(params IGenerator[] generators)
        {
            if (generators == null)
            {
                throw new ArgumentNullException();
            }

            this.generators = generators;
            this.queuedTokens = new Queue<IToken>();
        }

        public bool HasTokens
        {
            get { return this.queuedTokens.Any(); }
        }

        public static Emitter GetInstance(params IGenerator[] generators)
        {
            return new Emitter(generators);
        }

        public void Process(IToken token)
        {
            // Generate tokens if needed
            foreach (var generator in this.generators)
            {
                if (generator.IsTrigger(token))
                {
                    foreach (var generated in generator.Generate(token))
                    {
                        this.queuedTokens.Enqueue(generated);
                    }
                }
            }

            // Preserve original token
            this.queuedTokens.Enqueue(token);
        }

        public IToken NextToken()
        {
            return this.queuedTokens.Dequeue();
        }
    }
}
