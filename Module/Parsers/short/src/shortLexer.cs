//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 3.4.1.9004
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// $ANTLR 3.4.1.9004 D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3 2013-01-14 23:59:15

// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 219
// Unreachable code detected.
#pragma warning disable 162
// Missing XML comment for publicly visible type or member 'Type_or_Member'
#pragma warning disable 1591
// CLS compliance checking will not be performed on 'type' because it is not visible from outside this assembly.
#pragma warning disable 3019

using Automation.Core;

using System.Collections.Generic;
using Antlr.Runtime;
using Antlr.Runtime.Misc;

namespace Automation.Parsers.shortGrammar
{
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "3.4.1.9004")]
[System.CLSCompliant(false)]
public partial class shortLexer : Antlr.Runtime.Lexer
{
	public const int EOF=-1;
	public const int FILE=4;
	public const int NEWLINE=5;
	public const int SECTION=6;
	public const int WORD=7;
	public const int WS=8;
	public const int T__9=9;
	public const int T__10=10;

    // delegates
    // delegators

	public shortLexer()
	{
		OnCreated();
	}

	public shortLexer(ICharStream input )
		: this(input, new RecognizerSharedState())
	{
	}

	public shortLexer(ICharStream input, RecognizerSharedState state)
		: base(input, state)
	{

		OnCreated();
	}
	public override string GrammarFileName { get { return "D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3"; } }


	partial void OnCreated();
	partial void EnterRule(string ruleName, int ruleIndex);
	partial void LeaveRule(string ruleName, int ruleIndex);

	partial void EnterRule_T__9();
	partial void LeaveRule_T__9();

	// $ANTLR start "T__9"
	[GrammarRule("T__9")]
	private void mT__9()
	{
		EnterRule_T__9();
		EnterRule("T__9", 1);
		TraceIn("T__9", 1);
		try
		{
			int _type = T__9;
			int _channel = DefaultTokenChannel;
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:11:6: ( 'end' )
			DebugEnterAlt(1);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:11:8: 'end'
			{
			DebugLocation(11, 8);
			Match("end"); 


			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("T__9", 1);
			LeaveRule("T__9", 1);
			LeaveRule_T__9();
		}
	}
	// $ANTLR end "T__9"

	partial void EnterRule_T__10();
	partial void LeaveRule_T__10();

	// $ANTLR start "T__10"
	[GrammarRule("T__10")]
	private void mT__10()
	{
		EnterRule_T__10();
		EnterRule("T__10", 2);
		TraceIn("T__10", 2);
		try
		{
			int _type = T__10;
			int _channel = DefaultTokenChannel;
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:12:7: ( 'start' )
			DebugEnterAlt(1);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:12:9: 'start'
			{
			DebugLocation(12, 9);
			Match("start"); 


			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("T__10", 2);
			LeaveRule("T__10", 2);
			LeaveRule_T__10();
		}
	}
	// $ANTLR end "T__10"

	partial void EnterRule_WORD();
	partial void LeaveRule_WORD();

	// $ANTLR start "WORD"
	[GrammarRule("WORD")]
	private void mWORD()
	{
		EnterRule_WORD();
		EnterRule("WORD", 3);
		TraceIn("WORD", 3);
		try
		{
			int _type = WORD;
			int _channel = DefaultTokenChannel;
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:28:5: ( ( 'a' .. 'z' )+ )
			DebugEnterAlt(1);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:28:7: ( 'a' .. 'z' )+
			{
			DebugLocation(28, 7);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:28:7: ( 'a' .. 'z' )+
			int cnt1=0;
			try { DebugEnterSubRule(1);
			while (true)
			{
				int alt1=2;
				try { DebugEnterDecision(1, false);
				int LA1_1 = input.LA(1);

				if (((LA1_1>='a' && LA1_1<='z')))
				{
					alt1 = 1;
				}


				} finally { DebugExitDecision(1); }
				switch (alt1)
				{
				case 1:
					DebugEnterAlt(1);
					// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:
					{
					DebugLocation(28, 7);
					input.Consume();


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


			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("WORD", 3);
			LeaveRule("WORD", 3);
			LeaveRule_WORD();
		}
	}
	// $ANTLR end "WORD"

	partial void EnterRule_NEWLINE();
	partial void LeaveRule_NEWLINE();

	// $ANTLR start "NEWLINE"
	[GrammarRule("NEWLINE")]
	private void mNEWLINE()
	{
		EnterRule_NEWLINE();
		EnterRule("NEWLINE", 4);
		TraceIn("NEWLINE", 4);
		try
		{
			int _type = NEWLINE;
			int _channel = DefaultTokenChannel;
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:36:5: ( ( '\\r' )? '\\n' )
			DebugEnterAlt(1);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:36:7: ( '\\r' )? '\\n'
			{
			DebugLocation(36, 7);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:36:7: ( '\\r' )?
			int alt2=2;
			try { DebugEnterSubRule(2);
			try { DebugEnterDecision(2, false);
			int LA2_1 = input.LA(1);

			if ((LA2_1=='\r'))
			{
				alt2 = 1;
			}
			} finally { DebugExitDecision(2); }
			switch (alt2)
			{
			case 1:
				DebugEnterAlt(1);
				// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:36:7: '\\r'
				{
				DebugLocation(36, 7);
				Match('\r'); 

				}
				break;

			}
			} finally { DebugExitSubRule(2); }

			DebugLocation(36, 13);
			Match('\n'); 
			DebugLocation(36, 18);
			 _channel = Hidden; 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("NEWLINE", 4);
			LeaveRule("NEWLINE", 4);
			LeaveRule_NEWLINE();
		}
	}
	// $ANTLR end "NEWLINE"

	partial void EnterRule_WS();
	partial void LeaveRule_WS();

	// $ANTLR start "WS"
	[GrammarRule("WS")]
	private void mWS()
	{
		EnterRule_WS();
		EnterRule("WS", 5);
		TraceIn("WS", 5);
		try
		{
			int _type = WS;
			int _channel = DefaultTokenChannel;
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:38:4: ( ( ' ' | '\\t' | NEWLINE )+ )
			DebugEnterAlt(1);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:38:6: ( ' ' | '\\t' | NEWLINE )+
			{
			DebugLocation(38, 6);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:38:6: ( ' ' | '\\t' | NEWLINE )+
			int cnt3=0;
			try { DebugEnterSubRule(3);
			while (true)
			{
				int alt3=4;
				try { DebugEnterDecision(3, false);
				switch (input.LA(1))
				{
				case ' ':
					{
					alt3 = 1;
					}
					break;
				case '\t':
					{
					alt3 = 2;
					}
					break;
				case '\n':
				case '\r':
					{
					alt3 = 3;
					}
					break;
				}

				} finally { DebugExitDecision(3); }
				switch (alt3)
				{
				case 1:
					DebugEnterAlt(1);
					// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:38:7: ' '
					{
					DebugLocation(38, 7);
					Match(' '); 

					}
					break;
				case 2:
					DebugEnterAlt(2);
					// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:38:13: '\\t'
					{
					DebugLocation(38, 13);
					Match('\t'); 

					}
					break;
				case 3:
					DebugEnterAlt(3);
					// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:38:20: NEWLINE
					{
					DebugLocation(38, 20);
					mNEWLINE(); 

					}
					break;

				default:
					if (cnt3 >= 1)
						goto loop3;

					EarlyExitException eee3 = new EarlyExitException( 3, input );
					DebugRecognitionException(eee3);
					throw eee3;
				}
				cnt3++;
			}
			loop3:
				;

			} finally { DebugExitSubRule(3); }

			DebugLocation(38, 30);
			 _channel = Hidden; 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("WS", 5);
			LeaveRule("WS", 5);
			LeaveRule_WS();
		}
	}
	// $ANTLR end "WS"

	public override void mTokens()
	{
		// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:1:8: ( T__9 | T__10 | WORD | NEWLINE | WS )
		int alt4=5;
		try { DebugEnterDecision(4, false);
		switch (input.LA(1))
		{
		case 'e':
			{
			int LA4_2 = input.LA(2);

			if ((LA4_2=='n'))
			{
				int LA4_3 = input.LA(3);

				if ((LA4_3=='d'))
				{
					int LA4_4 = input.LA(4);

					if (((LA4_4>='a' && LA4_4<='z')))
					{
						alt4 = 3;
					}
					else
					{
						alt4 = 1;
					}
				}
				else
				{
					alt4 = 3;
				}
			}
			else
			{
				alt4 = 3;
			}
			}
			break;
		case 's':
			{
			int LA4_2 = input.LA(2);

			if ((LA4_2=='t'))
			{
				int LA4_3 = input.LA(3);

				if ((LA4_3=='a'))
				{
					int LA4_4 = input.LA(4);

					if ((LA4_4=='r'))
					{
						int LA4_5 = input.LA(5);

						if ((LA4_5=='t'))
						{
							int LA4_6 = input.LA(6);

							if (((LA4_6>='a' && LA4_6<='z')))
							{
								alt4 = 3;
							}
							else
							{
								alt4 = 2;
							}
						}
						else
						{
							alt4 = 3;
						}
					}
					else
					{
						alt4 = 3;
					}
				}
				else
				{
					alt4 = 3;
				}
			}
			else
			{
				alt4 = 3;
			}
			}
			break;
		case 'a':
		case 'b':
		case 'c':
		case 'd':
		case 'f':
		case 'g':
		case 'h':
		case 'i':
		case 'j':
		case 'k':
		case 'l':
		case 'm':
		case 'n':
		case 'o':
		case 'p':
		case 'q':
		case 'r':
		case 't':
		case 'u':
		case 'v':
		case 'w':
		case 'x':
		case 'y':
		case 'z':
			{
			alt4 = 3;
			}
			break;
		case '\r':
			{
			int LA4_2 = input.LA(2);

			if ((LA4_2=='\n'))
			{
				int LA4_3 = input.LA(3);

				if (((LA4_3>='\t' && LA4_3<='\n')||LA4_3=='\r'||LA4_3==' '))
				{
					alt4 = 5;
				}
				else
				{
					alt4 = 4;
				}
			}
			else
			{
				NoViableAltException nvae = new NoViableAltException("", 4, 4, input, 2);
				DebugRecognitionException(nvae);
				throw nvae;
			}
			}
			break;
		case '\n':
			{
			int LA4_2 = input.LA(2);

			if (((LA4_2>='\t' && LA4_2<='\n')||LA4_2=='\r'||LA4_2==' '))
			{
				alt4 = 5;
			}
			else
			{
				alt4 = 4;
			}
			}
			break;
		case '\t':
		case ' ':
			{
			alt4 = 5;
			}
			break;
		default:
			{
				NoViableAltException nvae = new NoViableAltException("", 4, 0, input, 1);
				DebugRecognitionException(nvae);
				throw nvae;
			}
		}

		} finally { DebugExitDecision(4); }
		switch (alt4)
		{
		case 1:
			DebugEnterAlt(1);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:1:10: T__9
			{
			DebugLocation(1, 10);
			mT__9(); 

			}
			break;
		case 2:
			DebugEnterAlt(2);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:1:15: T__10
			{
			DebugLocation(1, 15);
			mT__10(); 

			}
			break;
		case 3:
			DebugEnterAlt(3);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:1:21: WORD
			{
			DebugLocation(1, 21);
			mWORD(); 

			}
			break;
		case 4:
			DebugEnterAlt(4);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:1:26: NEWLINE
			{
			DebugLocation(1, 26);
			mNEWLINE(); 

			}
			break;
		case 5:
			DebugEnterAlt(5);
			// D:\\Archive\\Projects\\AntlrAutomation\\Module\\Parsers\\short\\short.g3:1:34: WS
			{
			DebugLocation(1, 34);
			mWS(); 

			}
			break;

		}

	}


	#region DFA

	protected override void InitDFAs()
	{
		base.InitDFAs();
	}

 
	#endregion

}

} // namespace Automation.Parsers.shortGrammar
