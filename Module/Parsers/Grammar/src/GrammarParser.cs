//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 3.4.1.9004
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// $ANTLR 3.4.1.9004 D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\Grammar\\Grammar.g3 2012-12-13 23:53:08

// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 219
// Unreachable code detected.
#pragma warning disable 162
// Missing XML comment for publicly visible type or member 'Type_or_Member'
#pragma warning disable 1591
// CLS compliance checking will not be performed on 'type' because it is not visible from outside this assembly.
#pragma warning disable 3019


using System.Collections.Generic;
using Antlr.Runtime;
using Antlr.Runtime.Misc;


using Antlr.Runtime.Tree;
using RewriteRuleITokenStream = Antlr.Runtime.Tree.RewriteRuleTokenStream;

namespace Sample.Parser
{
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "3.4.1.9004")]
[System.CLSCompliant(false)]
public partial class GrammarParser : Antlr.Runtime.Parser
{
	internal static readonly string[] tokenNames = new string[] {
		"<invalid>", "<EOR>", "<DOWN>", "<UP>", "FILE", "NEWLINE", "SECTION", "WORD", "WS", "'end'", "'start'"
	};
	public const int EOF=-1;
	public const int FILE=4;
	public const int NEWLINE=5;
	public const int SECTION=6;
	public const int WORD=7;
	public const int WS=8;
	public const int T__9=9;
	public const int T__10=10;

	public GrammarParser(ITokenStream input)
		: this(input, new RecognizerSharedState())
	{
	}
	public GrammarParser(ITokenStream input, RecognizerSharedState state)
		: base(input, state)
	{
		ITreeAdaptor treeAdaptor = default(ITreeAdaptor);
		CreateTreeAdaptor(ref treeAdaptor);
		TreeAdaptor = treeAdaptor ?? new CommonTreeAdaptor();
		OnCreated();
	}
	// Implement this function in your helper file to use a custom tree adaptor
	partial void CreateTreeAdaptor(ref ITreeAdaptor adaptor);

	private ITreeAdaptor adaptor;

	public ITreeAdaptor TreeAdaptor
	{
		get
		{
			return adaptor;
		}

		set
		{
			this.adaptor = value;
		}
	}

	public override string[] TokenNames { get { return GrammarParser.tokenNames; } }
	public override string GrammarFileName { get { return "D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\Grammar\\Grammar.g3"; } }


	partial void OnCreated();
	partial void EnterRule(string ruleName, int ruleIndex);
	partial void LeaveRule(string ruleName, int ruleIndex);

	#region Rules
	partial void EnterRule_file();
	partial void LeaveRule_file();
	// $ANTLR start "file"
	// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\Grammar\\Grammar.g3:20:8: public file : ( section )+ EOF -> ^( FILE ( section )+ ) ;
	[GrammarRule("file")]
	public AstParserRuleReturnScope<CommonTree, CommonToken> file()
	{
		EnterRule_file();
		EnterRule("file", 1);
		TraceIn("file", 1);
		AstParserRuleReturnScope<CommonTree, CommonToken> retval = new AstParserRuleReturnScope<CommonTree, CommonToken>();
		retval.Start = (CommonToken)input.LT(1);

		CommonTree root_0 = default(CommonTree);

		CommonToken EOF2 = default(CommonToken);
		AstParserRuleReturnScope<CommonTree, CommonToken> section1 = default(AstParserRuleReturnScope<CommonTree, CommonToken>);

		CommonTree EOF2_tree = default(CommonTree);
		RewriteRuleITokenStream stream_EOF=new RewriteRuleITokenStream(adaptor,"token EOF");
		RewriteRuleSubtreeStream stream_section=new RewriteRuleSubtreeStream(adaptor,"rule section");
		try { DebugEnterRule(GrammarFileName, "file");
		DebugLocation(20, 4);
		try
		{
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\Grammar\\Grammar.g3:21:5: ( ( section )+ EOF -> ^( FILE ( section )+ ) )
			DebugEnterAlt(1);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\Grammar\\Grammar.g3:21:7: ( section )+ EOF
			{
			DebugLocation(21, 7);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\Grammar\\Grammar.g3:21:7: ( section )+
			int cnt1=0;
			try { DebugEnterSubRule(1);
			while (true)
			{
				int alt1=2;
				try { DebugEnterDecision(1, false);
				int LA1_1 = input.LA(1);

				if ((LA1_1==10))
				{
					alt1 = 1;
				}


				} finally { DebugExitDecision(1); }
				switch (alt1)
				{
				case 1:
					DebugEnterAlt(1);
					// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\Grammar\\Grammar.g3:21:7: section
					{
					DebugLocation(21, 7);
					PushFollow(Follow._section_in_file85);
					section1=section();
					PopFollow();

					stream_section.Add(section1.Tree);

					}
					break;

				default:
					if (cnt1 >= 1)
						goto loop1;

					EarlyExitException eee1 = new EarlyExitException( 1, input );
					DebugRecognitionException(eee1);
					throw eee1;
				}
				cnt1++;
			}
			loop1:
				;

			} finally { DebugExitSubRule(1); }

			DebugLocation(21, 16);
			EOF2=(CommonToken)Match(input,EOF,Follow._EOF_in_file88);  
			stream_EOF.Add(EOF2);



			{
			// AST REWRITE
			// elements: section
			// token labels: 
			// rule labels: retval
			// token list labels: 
			// rule list labels: 
			// wildcard labels: 
			retval.Tree = root_0;
			RewriteRuleSubtreeStream stream_retval=new RewriteRuleSubtreeStream(adaptor,"rule retval",retval!=null?retval.Tree:null);

			root_0 = (CommonTree)adaptor.Nil();
			// 21:20: -> ^( FILE ( section )+ )
			{
				DebugLocation(21, 23);
				// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\Grammar\\Grammar.g3:21:23: ^( FILE ( section )+ )
				{
				CommonTree root_1 = (CommonTree)adaptor.Nil();
				DebugLocation(21, 25);
				root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(FILE, "FILE"), root_1);

				DebugLocation(21, 30);
				if (!(stream_section.HasNext))
				{
					throw new RewriteEarlyExitException();
				}
				while ( stream_section.HasNext )
				{
					DebugLocation(21, 30);
					adaptor.AddChild(root_1, stream_section.NextTree());

				}
				stream_section.Reset();

				adaptor.AddChild(root_0, root_1);
				}

			}

			retval.Tree = root_0;
			}

			}

			retval.Stop = (CommonToken)input.LT(-1);

			retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
			adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

		}
		catch (RecognitionException re)
		{
			ReportError(re);
			Recover(input,re);
		retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

		}
		finally
		{
			TraceOut("file", 1);
			LeaveRule("file", 1);
			LeaveRule_file();
		}
		DebugLocation(22, 4);
		} finally { DebugExitRule(GrammarFileName, "file"); }
		return retval;

	}
	// $ANTLR end "file"

	partial void EnterRule_section();
	partial void LeaveRule_section();
	// $ANTLR start "section"
	// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\Grammar\\Grammar.g3:24:1: section : 'start' ( WORD )* 'end' -> ^( SECTION ( WORD )* ) ;
	[GrammarRule("section")]
	private AstParserRuleReturnScope<CommonTree, CommonToken> section()
	{
		EnterRule_section();
		EnterRule("section", 2);
		TraceIn("section", 2);
		AstParserRuleReturnScope<CommonTree, CommonToken> retval = new AstParserRuleReturnScope<CommonTree, CommonToken>();
		retval.Start = (CommonToken)input.LT(1);

		CommonTree root_0 = default(CommonTree);

		CommonToken string_literal3 = default(CommonToken);
		CommonToken WORD4 = default(CommonToken);
		CommonToken string_literal5 = default(CommonToken);

		CommonTree string_literal3_tree = default(CommonTree);
		CommonTree WORD4_tree = default(CommonTree);
		CommonTree string_literal5_tree = default(CommonTree);
		RewriteRuleITokenStream stream_10=new RewriteRuleITokenStream(adaptor,"token 10");
		RewriteRuleITokenStream stream_WORD=new RewriteRuleITokenStream(adaptor,"token WORD");
		RewriteRuleITokenStream stream_9=new RewriteRuleITokenStream(adaptor,"token 9");
		try { DebugEnterRule(GrammarFileName, "section");
		DebugLocation(24, 4);
		try
		{
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\Grammar\\Grammar.g3:25:5: ( 'start' ( WORD )* 'end' -> ^( SECTION ( WORD )* ) )
			DebugEnterAlt(1);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\Grammar\\Grammar.g3:25:7: 'start' ( WORD )* 'end'
			{
			DebugLocation(25, 7);
			string_literal3=(CommonToken)Match(input,10,Follow._10_in_section114);  
			stream_10.Add(string_literal3);

			DebugLocation(25, 15);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\Grammar\\Grammar.g3:25:15: ( WORD )*
			try { DebugEnterSubRule(2);
			while (true)
			{
				int alt2=2;
				try { DebugEnterDecision(2, false);
				int LA2_1 = input.LA(1);

				if ((LA2_1==WORD))
				{
					alt2 = 1;
				}


				} finally { DebugExitDecision(2); }
				switch ( alt2 )
				{
				case 1:
					DebugEnterAlt(1);
					// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\Grammar\\Grammar.g3:25:15: WORD
					{
					DebugLocation(25, 15);
					WORD4=(CommonToken)Match(input,WORD,Follow._WORD_in_section116);  
					stream_WORD.Add(WORD4);


					}
					break;

				default:
					goto loop2;
				}
			}

			loop2:
				;

			} finally { DebugExitSubRule(2); }

			DebugLocation(25, 21);
			string_literal5=(CommonToken)Match(input,9,Follow._9_in_section119);  
			stream_9.Add(string_literal5);



			{
			// AST REWRITE
			// elements: WORD
			// token labels: 
			// rule labels: retval
			// token list labels: 
			// rule list labels: 
			// wildcard labels: 
			retval.Tree = root_0;
			RewriteRuleSubtreeStream stream_retval=new RewriteRuleSubtreeStream(adaptor,"rule retval",retval!=null?retval.Tree:null);

			root_0 = (CommonTree)adaptor.Nil();
			// 25:27: -> ^( SECTION ( WORD )* )
			{
				DebugLocation(25, 30);
				// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\Grammar\\Grammar.g3:25:30: ^( SECTION ( WORD )* )
				{
				CommonTree root_1 = (CommonTree)adaptor.Nil();
				DebugLocation(25, 32);
				root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(SECTION, "SECTION"), root_1);

				DebugLocation(25, 40);
				// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\Grammar\\Grammar.g3:25:40: ( WORD )*
				while ( stream_WORD.HasNext )
				{
					DebugLocation(25, 40);
					adaptor.AddChild(root_1, stream_WORD.NextNode());

				}
				stream_WORD.Reset();

				adaptor.AddChild(root_0, root_1);
				}

			}

			retval.Tree = root_0;
			}

			}

			retval.Stop = (CommonToken)input.LT(-1);

			retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
			adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

		}
		catch (RecognitionException re)
		{
			ReportError(re);
			Recover(input,re);
		retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

		}
		finally
		{
			TraceOut("section", 2);
			LeaveRule("section", 2);
			LeaveRule_section();
		}
		DebugLocation(26, 4);
		} finally { DebugExitRule(GrammarFileName, "section"); }
		return retval;

	}
	// $ANTLR end "section"
	#endregion Rules


	#region Follow sets
	private static class Follow
	{
		public static readonly BitSet _section_in_file85 = new BitSet(new ulong[]{0x0UL});
		public static readonly BitSet _EOF_in_file88 = new BitSet(new ulong[]{0x2UL});
		public static readonly BitSet _10_in_section114 = new BitSet(new ulong[]{0x280UL});
		public static readonly BitSet _WORD_in_section116 = new BitSet(new ulong[]{0x280UL});
		public static readonly BitSet _9_in_section119 = new BitSet(new ulong[]{0x2UL});
	}
	#endregion Follow sets
}

} // namespace Sample.Parser
