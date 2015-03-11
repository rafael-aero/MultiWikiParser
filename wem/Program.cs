﻿
namespace wem
{


	class MainClass
	{


		public class XhtmlPrinter : org.wikimodel.wem.IWikiPrinter, System.IDisposable
		{
			protected bool disposed = false;
			protected System.Text.StringBuilder sb = new System.Text.StringBuilder();


			// Public implementation of Dispose pattern callable by consumers. 
			public void Dispose()
			{ 
				Dispose(true);
				System.GC.SuppressFinalize(this);           
			}


			// Protected implementation of Dispose pattern. 
			protected virtual void Dispose(bool disposing)
			{
				if (disposed)
					return; 

				if (disposing) 
				{
					// Free any other managed objects here. 
					if (sb != null)
					{
						sb.Clear ();
						sb = null;
					}
				}

				// Free any unmanaged objects here. 
				disposed = true;
			}


			public void print(string str) 
			{
				sb.Append(str);
			}


			public void println(string str) 
			{
				sb.Append(str);
				sb.Append("\n");
			}


			public string Text
			{
				get
				{ 
					return sb.ToString();
				}

			}

		}


		public static string WikiToHtml(string markup)
		{
			string retVal = null;

			using(XhtmlPrinter printer = new XhtmlPrinter())
			{
				java.io.Reader rdr = new java.io.StringReader(markup);

				// org.wikimodel.wem.WikiPrinter wp = new org.wikimodel.wem.WikiPrinter ();
				// var listener = new org.wikimodel.wem.xwiki.XWikiSerializer(wp);

				// var listener = new org.wikimodel.wem.xwiki.XWikiSerializer(printer);
				org.wikimodel.wem.IWemListener listener = new org.wikimodel.wem.xhtml.PrintListener (printer);

				org.wikimodel.wem.mediawiki.MediaWikiParser mep = 
					new org.wikimodel.wem.mediawiki.MediaWikiParser ();
				mep.parse (rdr, listener);
				retVal = printer.Text;

				rdr.close ();
				rdr = null;
				listener = null;
				mep = null;
			} // End Using printer

			return retVal;
		}


		public static void Main(string[] args)
		{
			string str =@"
<languages />
{{TNT|PD Help Page}}
<translate>
<!--T:1-->
You can format your text by using wiki markup. This consists of normal characters like asterisks, apostrophes or equal signs which have a special function in the wiki, sometimes depending on their position. For example, to format a word in ''italic'', you include it in two pairs of apostrophes like <code><nowiki>''this''</nowiki></code>.

== Text formatting markup == <!--T:2-->
</translate>
{| class=""wikitable""
! <translate><!--T:3-->
Description</translate>
! width=40% | <translate><!--T:4-->
You type</translate>
! width=40% | <translate><!--T:5-->
You get</translate>
|-
! colspan=""3"" style=""background: #ABE"" | <translate><!--T:6-->
Character (inline) formatting – ''applies anywhere''</translate>
|-
| <translate><!--T:7-->
Italic text</translate>
| <pre>
''<translate><!--T:8-->
italic</translate>''
</pre>
|
''<translate><!--T:9-->
italic</translate>''
|-
| <translate><!--T:10-->
Bold text</translate>
| <pre>
'''<translate><!--T:11-->
bold</translate>'''
</pre>
|
'''<translate><!--T:12-->
bold</translate>'''
|-
| <translate><!--T:13-->
Bold and italic</translate>
| <pre>
'''''<translate><!--T:14-->
bold & italic</translate>'''''
</pre>
|
'''''<translate><!--T:15-->
bold & italic</translate>'''''
|-
| <translate><!--T:16-->
Strike text</translate>
| <pre>
<strike> <translate><!--T:17-->
strike text</translate> </strike>
</pre>
|<strike> <translate><!--T:18-->
strike text</translate> </strike>
|-
| <translate><!--T:19-->
Escape wiki markup</translate>
| <pre>
&lt;nowiki&gt;<translate><!--T:20-->
no ''markup''</translate>&lt;/nowiki&gt;
</pre>
|
<translate><!--T:155-->
<nowiki>no ''markup''</nowiki></translate>
|-
| <translate><!--T:21-->
Escape wiki markup once</translate>
| <pre>
[[Special:MyLanguage/API:Main page|API]]&lt;nowiki/>&nbsp;<translate><!--T:22-->
extension</translate>
</pre>
|
[[Special:MyLanguage/API:Main page|API]]<nowiki/>&nbsp;<translate><!--T:23-->
extension</translate>
|-
! colspan=""3"" style=""background: #ABE"" | <translate><!--T:24-->
Section formatting – ''only at the beginning of the line''</translate>
|-
| <translate><!--T:25-->
Headings of different levels</translate> 
| <pre>
<translate>
== Level 2 == <!--T:26-->

=== Level 3 === <!--T:27-->

==== Level 4 ==== <!--T:28-->

===== Level 5 ===== <!--T:29-->

====== Level 6 ====== <!--T:30-->
</translate>
</pre>
----
{{note|
* <translate><!--T:31-->
[[<tvar|lv1>Special:MyLanguage/Help_talk:Formatting#Level_1</>|Skip Level 1]], it is page name level.</translate>
* <translate><!--T:33-->
An article with 4 or more headings automatically creates a [[wikipedia:Wikipedia:Section#Table of contents (TOC)|table of contents]].</translate>
}}
|
<!-- using HTML markup to avoid creating new sections -->
<translate>
<!--T:35-->
<h2>Level 2</h2>

<!--T:36-->
<h3>Level 3</h3>

<!--T:37-->
<h4>Level 4</h4>

<!--T:38-->
<h5>Level 5</h5>

<!--T:39-->
<h6>Level 6</h6>
</translate>
|-
| <translate><!--T:40-->
Horizontal rule</translate>
| <pre>
<translate><!--T:41-->
Text before</translate>
----
<translate><!--T:42-->
Text after</translate>
</pre>
|
<translate><!--T:43-->
Text before</translate>
----
<translate><!--T:44-->
Text after</translate>
|-
| <translate><!--T:45-->
Bullet list</translate>
|
<pre>
<translate>
<!--T:46-->
* Start each line
* with an [[Wikipedia:asterisk|asterisk]] (*).
** More asterisks give deeper
*** and deeper levels.
* Line breaks <br />don't break levels.
*** But jumping levels creates empty space.
Any other start ends the list.
</translate>
</pre>
|
<translate>
<!--T:47-->
* Start each line
* with an [[Wikipedia:asterisk|asterisk]] (*).
** More asterisks give deeper
*** and deeper levels.
* Line breaks <br />don't break levels.
*** But jumping levels creates empty space.
Any other start ends the list.
</translate>
|-
| <translate><!--T:48-->
Numbered list</translate>
|
<pre>
<translate>
<!--T:49-->
# Start each line
# with a [[Wikipedia:Number_sign|number sign]] (#).
## More number signs give deeper
### and deeper
### levels.
# Line breaks <br />don't break levels.
### But jumping levels creates empty space.
# Blank lines

<!--T:50-->
# end the list and start another.
Any other start also
ends the list.
</translate>
</pre>
|
<translate>
<!--T:51-->
# Start each line
# with a [[Wikipedia:Number_sign|number sign]] (#).
## More number signs give deeper
### and deeper
### levels.
# Line breaks <br />don't break levels.
### But jumping levels creates empty space.
# Blank lines

<!--T:52-->
# end the list and start another.
Any other start also
ends the list.
</translate>
|-
| <translate><!--T:53-->
Definition list</translate>
| <pre>
<translate>
<!--T:54-->
;item 1
: definition 1
;item 2
: definition 2-1
: definition 2-2
</translate>
</pre>
|
<translate>
<!--T:55-->
;item 1
: definition 1
;item 2
: definition 2-1
: definition 2-2
</translate>
|-
| <translate><!--T:56-->
Indent text</translate>
| <pre>
<translate>
<!--T:57-->
: Single indent
:: Double indent
::::: Multiple indent
</translate>
</pre>
----
{{Note|<translate><!--T:58-->
This workaround may harm accessibility.</translate>}}
|
<translate>
<!--T:59-->
: Single indent
:: Double indent
::::: Multiple indent
</translate>
|-
| <translate><!--T:60-->
Mixture of different types of list</translate>
|
<pre>
<translate>
<!--T:61-->
# one
# two
#* two point one
#* two point two
# three
#; three item one
#: three def one
# four
#: four def one
#: this looks like a continuation
#: and is often used
#: instead <br />of &lt;nowiki><br />&lt;/nowiki>
# five
## five sub 1
### five sub 1 sub 1
## five sub 2
</translate>
</pre>
----
{{note|<translate><!--T:62-->
The usage of <code>#:</code> and <code>*:</code> for breaking a line within an item may also harm accessibility.</translate>}}
|
<translate>
<!--T:63-->
# one
# two
#* two point one
#* two point two
# three
#; three item one
#: three def one
# four
#: four def one
#: this looks like a continuation
#: and is often used
#: instead <br />of <nowiki><br /></nowiki>
# five
## five sub 1
### five sub 1 sub 1
## five sub 2{{anchor|pre}}
</translate>
|-
| <translate><!--T:64-->
Preformatted text</translate>
| <pre>
<translate>
 <!--T:65-->
Start each line with a space.
 Text is '''preformatted''' and
 ''markups'' '''''can''''' be done.
</translate>
</pre>
----
{{note|<translate><!--T:66-->
This way of preformatting only applies to section formatting. Character formatting markups are still effective.</translate>}}
|
<translate>
 <!--T:67-->
Start each line with a space.
 Text is '''preformatted''' and
 ''markups'' '''''can''''' be done.
</translate>
|-
| <translate><!--T:68-->
Preformatted text blocks</translate>
| <pre> <translate><!--T:69-->
<nowiki><nowiki>Start with a space in the first column,
(before the <nowiki>).

<!--T:70-->
Then your block format will be
    maintained.
 
This is good for copying in code blocks:

<!--T:71-->
def function():
    """"""documentation string""""""

    <!--T:72-->
if True:
        print True
    else:
        print False</nowiki></nowiki></translate>
</pre>
|
 <translate><!--T:73-->
<nowiki>Start with a space in the first column,
(before the <nowiki>).

<!--T:74-->
Then your block format will be
    maintained.

<!--T:75-->
This is good for copying in code blocks:

<!--T:76-->
def function():
    """"""documentation string""""""

    <!--T:77-->
if True:
        print True
    else:
        print False</nowiki></translate>
|}
<translate>
== Paragraphs == <!--T:78-->

<!--T:79-->
MediaWiki ignores single line breaks. To start a new paragraph, leave an empty line. You can force a line break within a paragraph with the HTML tag <code>&lt;br /></code>.

== HTML tags == <!--T:80-->

<!--T:81-->
Some [[wikipedia:HTML|HTML]] tags are allowed in MediaWiki, for example <code>&lt;code></code>, <code>&lt;div></code>, <code><nowiki><span></nowiki></code> and <code><nowiki><font></nowiki></code>. These apply anywhere you insert them.
</translate>
{| class=""wikitable""
! <translate><!--T:82-->
Description</translate>
! width=40% | <translate><!--T:83-->
You type</translate>
! width=40% | <translate><!--T:84-->
You get</translate>
|-
| <translate><!--T:85-->
Inserted <br />(Displays as underline in most browsers)</translate>
| <pre>
<ins><translate><!--T:86-->
Inserted</translate></ins>

<translate><!--T:87-->
or</translate>

<u><translate><!--T:88-->
Underline</translate></u>
</pre>
|
<ins><translate><!--T:89-->
Inserted</translate></ins>

<translate><!--T:90-->
or</translate>

<u><translate><!--T:91-->
Underline</translate></u>
|-
| <translate><!--T:92-->
Deleted <br />(Displays as strikethrough in most browsers)</translate>
| <pre>
<s><translate><!--T:93-->
Struck out</translate></s>

<translate><!--T:94-->
or</translate>

<del><translate><!--T:95-->
Deleted</translate></del>

</pre>
|
<s><translate><!--T:96-->
Struck out</translate></s>

<translate><!--T:97-->
or</translate>

<del><translate><!--T:98-->
Deleted</translate></del>
|-
| <translate><!--T:99-->
Fixed width text</translate>
| <pre>
<code><translate><!--T:100-->
Source code</translate></code>

<translate><!--T:101-->
or</translate>

<tt><translate><!--T:157-->
Fixed width text</translate></tt>
</pre>
|
<code><translate><!--T:154-->
Source code</translate></code>

<translate><!--T:102-->
or</translate>

<tt><translate><!--T:103-->
Fixed width text</translate></tt>
|-
| <translate><!--T:104-->
Blockquotes</translate>
| <pre>
<translate><!--T:105-->
Text before</translate>
<blockquote><translate><!--T:106-->
Blockquote</translate></blockquote>
<translate><!--T:107-->
Text after</translate>
</pre>
|
<translate><!--T:108-->
Text before</translate>
<blockquote><translate><!--T:109-->
Blockquote</translate></blockquote>
<translate><!--T:110-->
Text after</translate>
|-
| <translate><!--T:111-->
Comment</translate>
| <pre><translate>
<!--T:112-->
<!-- This is a comment -->
Comments are visible only 
in the edit zone.
</translate></pre>
|
<translate>
<!--T:113-->
<!-- This is a comment -->
Comments are visible only 
in the edit zone.
</translate>
|-
| <translate><!--T:114-->
Completely preformatted text</translate>
| <pre>
<pre><translate>
<!--T:115-->
Text is '''preformatted''' and 
''markups'' '''''cannot''''' be done&lt;/pre>
</translate></pre>
----
{{note|<translate><!--T:116-->
For marking up of preformatted text, check the ""Preformatted text"" entry at the end of the previous table.</translate>}} 
|
<pre><translate>
<!--T:117-->
Text is '''preformatted''' and 
''markups'' '''''cannot''''' be done
</translate></pre>
|-
| <translate><!--T:118-->
'''Customized''' preformatted text</translate>
| <pre>
<pre style=""color: red""><translate>
<!--T:119-->
Text is '''preformatted''' 
with a style and 
''markups'' '''''cannot''''' be done
&lt;/pre>
</translate></pre>
----
{{note|<translate><!--T:120-->
A CSS style can be named within the <code>style</code> property.</translate>}}
|
<pre style=""color: red""><translate>
<!--T:121-->
Text is '''preformatted''' 
with a style and 
''markups'' '''''cannot''''' be done
</translate></pre>
|}
<translate>
<!--T:122-->
continued:</translate>
{| class=""wikitable""
! <translate><!--T:123-->
Description</translate>
! width=40% | <translate><!--T:124-->
You type</translate>
! width=40% | <translate><!--T:125-->
You get</translate>
|-
| <translate><!--T:126-->
'''Customized''' preformatted text with text wrap according to available width</translate>
| <pre>
<pre style=""white-space: pre-wrap; 
white-space: -moz-pre-wrap; 
white-space: -pre-wrap; 
white-space: -o-pre-wrap; 
word-wrap: break-word;"">
<translate><!--T:127-->
This long sentence is used to demonstrate text wrapping. This additional sentence makes the text even longer.</translate>
&lt;/pre>
</pre>
|
<pre style=""white-space: pre-wrap; 
white-space: -moz-pre-wrap; 
white-space: -pre-wrap; 
white-space: -o-pre-wrap; 
word-wrap: break-word;"">
<translate><!--T:128-->
This long sentence is used to demonstrate text wrapping. This additional sentence makes the text even longer.</translate>
</pre>
|-
| <translate><!--T:129-->
Preformatted text with text wrap according to available width</translate>
| <pre>
<code>
<translate><!--T:130-->
This long sentence is used to demonstrate text wrapping. This additional sentence makes the text even longer.</translate>
</code>
</pre>
| <code>
<translate><!--T:131-->
This long sentence is used to demonstrate text wrapping. This additional sentence makes the text even longer.</translate>
</code>
|}
<translate>
== Inserting symbols == <!--T:132-->

<!--T:133-->
Symbols and other special characters not available on your keyboard can be inserted through a special sequence of characters. Those sequences are called HTML entities. For example, the following sequence (entity) '''&amp;rarr;''' when inserted will be shown as <ins>right arrow</ins> HTML symbol &rarr; and '''&amp;mdash;''' when inserted will be shown as an <ins>em dash</ins> HTML symbol &mdash;. </translate>
----
{{note|<translate><!--T:134-->
Hover over any character to find out the symbol that it produces.  Some symbols not available in the current font will appear as empty squares.</translate>}} 

{| class=""wikitable"" align=center width=100%
! colspan=32 | HTML symbol entities
|- align=center
| '''<span title=""&amp;Aacute;"">&Aacute;</span>
| '''<span title=""&amp;aacute;"">&aacute;</span>
| '''<span title=""&amp;Acirc;"">&Acirc;</span>
| '''<span title=""&amp;acirc;"">&acirc;</span>
| '''<span title=""&amp;acute;"">&acute;</span>
| '''<span title=""&amp;AElig;"">&AElig;</span>
| '''<span title=""&amp;aelig;"">&aelig;</span>
| '''<span title=""&amp;Agrave;"">&Agrave;</span>
| '''<span title=""&amp;agrave;"">&agrave;</span>
| '''<span title=""&amp;alefsym;"">&alefsym;</span>
| '''<span title=""&amp;Alpha;"">&Alpha;</span>
| '''<span title=""&amp;alpha;"">&alpha;</span>
| '''<span title=""&amp;amp;"">&amp;</span>
| '''<span title=""&amp;and;"">&and;</span>
| '''<span title=""&amp;ang;"">&ang;</span>
| '''<span title=""&amp;Aring;"">&Aring;</span>
| '''<span title=""&amp;aring;"">&aring;</span>
| '''<span title=""&amp;asymp;"">&asymp;</span>
| '''<span title=""&amp;Atilde;"">&Atilde;</span>
| '''<span title=""&amp;atilde;"">&atilde;</span>
| '''<span title=""&amp;Auml;"">&Auml;</span>
| '''<span title=""&amp;auml;"">&auml;</span>
| '''<span title=""&amp;bdquo;"">&bdquo;</span>
| '''<span title=""&amp;Beta;"">&Beta;</span>
| '''<span title=""&amp;beta;"">&beta;</span>
| '''<span title=""&amp;brvbar;"">&brvbar;</span>
| '''<span title=""&amp;bull;"">&bull;</span>
| '''<span title=""&amp;cap;"">&cap;</span>
| '''<span title=""&amp;Ccedil;"">&Ccedil;</span>
| '''<span title=""&amp;ccedil;"">&ccedil;</span>
| '''<span title=""&amp;cedil;"">&cedil;</span>
| '''<span title=""&amp;cent;"">&cent;</span>
|- align=center
| '''<span title=""&amp;Chi;"">&Chi;</span>
| '''<span title=""&amp;chi;"">&chi;</span>
| '''<span title=""&amp;circ;"">&circ;</span>
| '''<span title=""&amp;clubs;"">&clubs;</span>
| '''<span title=""&amp;cong;"">&cong;</span>
| '''<span title=""&amp;copy;"">&copy;</span>
| '''<span title=""&amp;crarr;"">&crarr;</span>
| '''<span title=""&amp;cup;"">&cup;</span>
| '''<span title=""&amp;curren;"">&curren;</span>
| '''<span title=""&amp;dagger;"">&dagger;</span>
| '''<span title=""&amp;Dagger;"">&Dagger;</span>
| '''<span title=""&amp;darr;"">&darr;</span>
| '''<span title=""&amp;dArr;"">&dArr;</span>
| '''<span title=""&amp;deg;"">&deg;</span>
| '''<span title=""&amp;Delta;"">&Delta;</span>
| '''<span title=""&amp;delta;"">&delta;</span>
| '''<span title=""&amp;diams;"">&diams;</span>
| '''<span title=""&amp;divide;"">&divide;</span>
| '''<span title=""&amp;Eacute;"">&Eacute;</span>
| '''<span title=""&amp;eacute;"">&eacute;</span>
| '''<span title=""&amp;Ecirc;"">&Ecirc;</span>
| '''<span title=""&amp;ecirc;"">&ecirc;</span>
| '''<span title=""&amp;Egrave;"">&Egrave;</span>
| '''<span title=""&amp;egrave;"">&egrave;</span>
| '''<span title=""&amp;empty;"">&empty;</span>
| '''<span title=""&amp;emsp;"">&emsp;</span>
| '''<span title=""&amp;ensp;"">&ensp;</span>
| '''<span title=""&amp;Epsilon;"">&Epsilon;</span>
| '''<span title=""&amp;epsilon;"">&epsilon;</span>
| '''<span title=""&amp;equiv;"">&equiv;</span>
| '''<span title=""&amp;Eta;"">&Eta;</span>
| '''<span title=""&amp;eta;"">&eta;</span>
|- align=center
| '''<span title=""&amp;ETH;"">&ETH;</span>
| '''<span title=""&amp;eth;"">&eth;</span>
| '''<span title=""&amp;Euml;"">&Euml;</span>
| '''<span title=""&amp;euml;"">&euml;</span>
| '''<span title=""&amp;euro;"">&euro;</span>
| '''<span title=""&amp;exist;"">&exist;</span>
| '''<span title=""&amp;fnof;"">&fnof;</span>
| '''<span title=""&amp;forall;"">&forall;</span>
| '''<span title=""&amp;frac12;"">&frac12;</span>
| '''<span title=""&amp;frac14;"">&frac14;</span>
| '''<span title=""&amp;frac34;"">&frac34;</span>
| '''<span title=""&amp;frasl;"">&frasl;</span>
| '''<span title=""&amp;Gamma;"">&Gamma;</span>
| '''<span title=""&amp;gamma;"">&gamma;</span>
| '''<span title=""&amp;ge;"">&ge;</span>
| '''<span title=""&amp;gt;"">&gt;</span>
| '''<span title=""&amp;harr;"">&harr;</span>
| '''<span title=""&amp;hArr;"">&hArr;</span>
| '''<span title=""&amp;hearts;"">&hearts;</span>
| '''<span title=""&amp;hellip;"">&hellip;</span>
| '''<span title=""&amp;Iacute;"">&Iacute;</span>
| '''<span title=""&amp;iacute;"">&iacute;</span>
| '''<span title=""&amp;Icirc;"">&Icirc;</span>
| '''<span title=""&amp;icirc;"">&icirc;</span>
| '''<span title=""&amp;iexcl;"">&iexcl;</span>
| '''<span title=""&amp;Igrave;"">&Igrave;</span>
| '''<span title=""&amp;igrave;"">&igrave;</span>
| '''<span title=""&amp;image;"">&image;</span>
| '''<span title=""&amp;infin;"">&infin;</span>
| '''<span title=""&amp;int;"">&int;</span>
| '''<span title=""&amp;Iota;"">&Iota;</span>
| '''<span title=""&amp;iota;"">&iota;</span>
|- align=center
| '''<span title=""&amp;iquest;"">&iquest;</span>
| '''<span title=""&amp;isin;"">&isin;</span>
| '''<span title=""&amp;Iuml;"">&Iuml;</span>
| '''<span title=""&amp;iuml;"">&iuml;</span>
| '''<span title=""&amp;Kappa;"">&Kappa;</span>
| '''<span title=""&amp;kappa;"">&kappa;</span>
| '''<span title=""&amp;Lambda;"">&Lambda;</span>
| '''<span title=""&amp;lambda;"">&lambda;</span>
| '''<span title=""&amp;lang;"">&lang;</span>
| '''<span title=""&amp;laquo;"">&laquo;</span>
| '''<span title=""&amp;larr;"">&larr;</span>
| '''<span title=""&amp;lArr;"">&lArr;</span>
| '''<span title=""&amp;lceil;"">&lceil;</span>
| '''<span title=""&amp;ldquo;"">&ldquo;</span>
| '''<span title=""&amp;le;"">&le;</span>
| '''<span title=""&amp;lfloor;"">&lfloor;</span>
| '''<span title=""&amp;lowast;"">&lowast;</span>
| '''<span title=""&amp;loz;"">&loz;</span>
| '''<span title=""&amp;lrm;"">&lrm;</span>
| '''<span title=""&amp;lsaquo;"">&lsaquo;</span>
| '''<span title=""&amp;lsquo;"">&lsquo;</span>
| '''<span title=""&amp;lt;"">&lt;</span>
| '''<span title=""&amp;macr;"">&macr;</span>
| '''<span title=""&amp;mdash;"">&mdash;</span>
| '''<span title=""&amp;micro;"">&micro;</span>
| '''<span title=""&amp;middot;"">&middot;</span>
| '''<span title=""&amp;minus;"">&minus;</span>
| '''<span title=""&amp;Mu;"">&Mu;</span>
| '''<span title=""&amp;mu;"">&mu;</span>
| '''<span title=""&amp;nabla;"">&nabla;</span>
| '''<span title=""&amp;nbsp;"">&nbsp;</span>
| '''<span title=""&amp;ndash;"">&ndash;</span>
|- align=center
| '''<span title=""&amp;ne;"">&ne;</span>
| '''<span title=""&amp;ni;"">&ni;</span>
| '''<span title=""&amp;not;"">&not;</span>
| '''<span title=""&amp;notin;"">&notin;</span>
| '''<span title=""&amp;nsub;"">&nsub;</span>
| '''<span title=""&amp;Ntilde;"">&Ntilde;</span>
| '''<span title=""&amp;ntilde;"">&ntilde;</span>
| '''<span title=""&amp;Nu;"">&Nu;</span>
| '''<span title=""&amp;nu;"">&nu;</span>
| '''<span title=""&amp;Oacute;"">&Oacute;</span>
| '''<span title=""&amp;oacute;"">&oacute;</span>
| '''<span title=""&amp;Ocirc;"">&Ocirc;</span>
| '''<span title=""&amp;ocirc;"">&ocirc;</span>
| '''<span title=""&amp;OElig;"">&OElig;</span>
| '''<span title=""&amp;oelig;"">&oelig;</span>
| '''<span title=""&amp;Ograve;"">&Ograve;</span>
| '''<span title=""&amp;ograve;"">&ograve;</span>
| '''<span title=""&amp;oline;"">&oline;</span>
| '''<span title=""&amp;Omega;"">&Omega;</span>
| '''<span title=""&amp;omega;"">&omega;</span>
| '''<span title=""&amp;Omicron;"">&Omicron;</span>
| '''<span title=""&amp;omicron;"">&omicron;</span>
| '''<span title=""&amp;oplus;"">&oplus;</span>
| '''<span title=""&amp;or;"">&or;</span>
| '''<span title=""&amp;ordf;"">&ordf;</span>
| '''<span title=""&amp;ordm;"">&ordm;</span>
| '''<span title=""&amp;Oslash;"">&Oslash;</span>
| '''<span title=""&amp;oslash;"">&oslash;</span>
| '''<span title=""&amp;Otilde;"">&Otilde;</span>
| '''<span title=""&amp;otilde;"">&otilde;</span>
| '''<span title=""&amp;otimes;"">&otimes;</span>
| '''<span title=""&amp;Ouml;"">&Ouml;</span>
|- align=center
| '''<span title=""&amp;ouml;"">&ouml;</span>
| '''<span title=""&amp;para;"">&para;</span>
| '''<span title=""&amp;part;"">&part;</span>
| '''<span title=""&amp;permil;"">&permil;</span>
| '''<span title=""&amp;perp;"">&perp;</span>
| '''<span title=""&amp;Phi;"">&Phi;</span>
| '''<span title=""&amp;phi;"">&phi;</span>
| '''<span title=""&amp;Pi;"">&Pi;</span>
| '''<span title=""&amp;pi;"">&pi;</span>
| '''<span title=""&amp;piv;"">&piv;</span>
| '''<span title=""&amp;plusmn;"">&plusmn;</span>
| '''<span title=""&amp;pound;"">&pound;</span>
| '''<span title=""&amp;prime;"">&prime;</span>
| '''<span title=""&amp;Prime;"">&Prime;</span>
| '''<span title=""&amp;prod;"">&prod;</span>
| '''<span title=""&amp;prop;"">&prop;</span>
| '''<span title=""&amp;Psi;"">&Psi;</span>
| '''<span title=""&amp;psi;"">&psi;</span>
| '''<span title=""&amp;quot;"">&quot;</span>
| '''<span title=""&amp;radic;"">&radic;</span>
| '''<span title=""&amp;rang;"">&rang;</span>
| '''<span title=""&amp;raquo;"">&raquo;</span>
| '''<span title=""&amp;rarr;"">&rarr;</span>
| '''<span title=""&amp;rArr;"">&rArr;</span>
| '''<span title=""&amp;rceil;"">&rceil;</span>
| '''<span title=""&amp;rdquo;"">&rdquo;</span>
| '''<span title=""&amp;real;"">&real;</span>
| '''<span title=""&amp;reg;"">&reg;</span>
| '''<span title=""&amp;rfloor;"">&rfloor;</span>
| '''<span title=""&amp;Rho;"">&Rho;</span>
| '''<span title=""&amp;rho;"">&rho;</span>
| '''<span title=""&amp;rlm;"">&rlm;</span>
|- align=center
| '''<span title=""&amp;rsaquo;"">&rsaquo;</span>
| '''<span title=""&amp;rsquo;"">&rsquo;</span>
| '''<span title=""&amp;sbquo;"">&sbquo;</span>
| '''<span title=""&amp;Scaron;"">&Scaron;</span>
| '''<span title=""&amp;scaron;"">&scaron;</span>
| '''<span title=""&amp;sdot;"">&sdot;</span>
| '''<span title=""&amp;sect;"">&sect;</span>
| '''<span title=""&amp;shy;"">&shy;</span>
| '''<span title=""&amp;Sigma;"">&Sigma;</span>
| '''<span title=""&amp;sigma;"">&sigma;</span>
| '''<span title=""&amp;sigmaf;"">&sigmaf;</span>
| '''<span title=""&amp;sim;"">&sim;</span>
| '''<span title=""&amp;spades;"">&spades;</span>
| '''<span title=""&amp;sub;"">&sub;</span>
| '''<span title=""&amp;sube;"">&sube;</span>
| '''<span title=""&amp;sum;"">&sum;</span>
| '''<span title=""&amp;sup;"">&sup;</span>
| '''<span title=""&amp;sup1;"">&sup1;</span>
| '''<span title=""&amp;sup2;"">&sup2;</span>
| '''<span title=""&amp;sup3;"">&sup3;</span>
| '''<span title=""&amp;supe;"">&supe;</span>
| '''<span title=""&amp;szlig;"">&szlig;</span>
| '''<span title=""&amp;Tau;"">&Tau;</span>
| '''<span title=""&amp;tau;"">&tau;</span>
| '''<span title=""&amp;there4;"">&there4;</span>
| '''<span title=""&amp;Theta;"">&Theta;</span>
| '''<span title=""&amp;theta;"">&theta;</span>
| '''<span title=""&amp;thetasym;"">&thetasym;</span>
| '''<span title=""&amp;thinsp;"">&thinsp;</span>
| '''<span title=""&amp;THORN;"">&THORN;</span>
| '''<span title=""&amp;thorn;"">&thorn;</span>
| '''<span title=""&amp;tilde;"">&tilde;</span>
|- align=center
| '''<span title=""&amp;times;"">&times;</span>
| '''<span title=""&amp;trade;"">&trade;</span>
| '''<span title=""&amp;Uacute;"">&Uacute;</span>
| '''<span title=""&amp;uacute;"">&uacute;</span>
| '''<span title=""&amp;uarr;"">&uarr;</span>
| '''<span title=""&amp;uArr;"">&uArr;</span>
| '''<span title=""&amp;Ucirc;"">&Ucirc;</span>
| '''<span title=""&amp;ucirc;"">&ucirc;</span>
| '''<span title=""&amp;Ugrave;"">&Ugrave;</span>
| '''<span title=""&amp;ugrave;"">&ugrave;</span>
| '''<span title=""&amp;uml;"">&uml;</span>
| '''<span title=""&amp;upsih;"">&upsih;</span>
| '''<span title=""&amp;Upsilon;"">&Upsilon;</span>
| '''<span title=""&amp;upsilon;"">&upsilon;</span>
| '''<span title=""&amp;Uuml;"">&Uuml;</span>
| '''<span title=""&amp;uuml;"">&uuml;</span>
| '''<span title=""&amp;weierp;"">&weierp;</span>
| '''<span title=""&amp;Xi;"">&Xi;</span>
| '''<span title=""&amp;xi;"">&xi;</span>
| '''<span title=""&amp;Yacute;"">&Yacute;</span>
| '''<span title=""&amp;yacute;"">&yacute;</span>
| '''<span title=""&amp;yen;"">&yen;</span>
| '''<span title=""&amp;yuml;"">&yuml;</span>
| '''<span title=""&amp;Yuml;"">&Yuml;</span>
| '''<span title=""&amp;Zeta;"">&Zeta;</span>
| '''<span title=""&amp;zeta;"">&zeta;</span>
| '''<span title=""&amp;zwj;"">&zwj;</span>
| '''<span title=""&amp;zwnj;"">&zwnj;</span>'''
|}

{| class=""wikitable""
! <translate><!--T:135-->
Description</translate>
! width=40% | <translate><!--T:136-->
You type</translate>
! width=40% | <translate><!--T:137-->
You get</translate>
|-
| <translate><!--T:138-->
Copyright symbol</translate>
| <tt><pre>
&amp;copy;
</pre></tt>
|
:::'''&copy;'''
|-
| <translate><!--T:139-->
Greek delta letter symbol</translate>
| <tt><pre>
&amp;delta;
</pre></tt>
|
:::'''&delta;'''
|-
| <translate><!--T:140-->
Euro currency symbol</translate>
| <tt><pre>
&amp;euro;
</pre></tt>
|
:::'''&euro;'''
|}
<translate>
<!--T:141-->
See the list of all HTML entities on the Wikipedia article [[wikipedia:List of HTML entities|List of HTML entities]]. Additionally, MediaWiki supports two non-standard entity reference sequences: <code>&amp;רלמ;</code> and <code>&amp;رلم;</code> which are both considered equivalent to <code>&amp;rlm;</code> which is a [[wikipedia:Right-to-left mark|right-to-left mark]]. (Used when combining right to left languages with left to right languages in the same page.)

== HTML tags and symbol entities displayed themselves (with and without interpreting them) == <!--T:142-->
</translate>
:<tt>&amp;amp;euro;</tt> &nbsp;&rarr; '''&amp;euro;'''

:<tt>&lt;span style=""color: red; text-decoration: line-through;""><translate><!--T:143-->
Typo to be corrected</translate>&lt;/span></tt> &nbsp;&rarr; '''<span style=""color: red; text-decoration: line-through;""><translate><!--T:144-->
Typo to be corrected</translate></span>'''

:<tt><translate><!--T:156-->
<nowiki>&amp;lt;span style=""color: red; text-decoration: line-through;"">Typo to be corrected</span></nowiki></translate></tt> &nbsp;&rarr; '''&lt;span style=""color: red; text-decoration: line-through;""><translate><!--T:145-->
Typo to be corrected</translate>&lt;/span>'''
<translate>
=== Nowiki for HTML === <!--T:146-->
</translate>
<<nowiki />nowiki /> <translate><!--T:147-->
can prohibit (HTML) tags:</translate>
* <nowiki><<</nowiki>nowiki />pre> &nbsp;&rarr; <<nowiki/>pre>
<translate><!--T:148-->
But ''not'' &amp; symbol escapes:</translate>
* &<<nowiki />nowiki />amp; &nbsp;&rarr; &amp;
<translate><!--T:149-->
To print &amp; symbol escapes as text, use ""<tt>&amp;amp;</tt>"" to replace the ""&"" character (eg. type ""<tt>&amp;amp;nbsp;</tt>"", which results in ""<tt>&amp;nbsp;</tt>"").

== Other formatting == <!--T:150--> 

<!--T:151-->
Beyond the text formatting markup shown hereinbefore, here are some other formatting references:
</translate>

* {{ll|Help:Links|nsp=0}}
<translate>
<!--T:152-->
* [[<tvar|lists>Special:MyLanguage/Help:Lists</>|Lists]]
* [[<tvar|images>Special:MyLanguage/Help:Images</>|Images]]
* References - see [[<tvar|citephp>Special:MyLanguage/Extension:Cite/Cite.php</>|Extension:Cite/Cite.php]]
* [[<tvar|tables>Special:MyLanguage/Help:Tables</>|Tables]]

<!--T:153-->
You can find more references at [[<tvar|help-contents>Special:MyLanguage/Help:Contents</>|Help:Contents]].
</translate>
[[Category:Help{{Langcat|Formatting}}|Formatting]]

";

			string strHTML = string.Format (@"<!DOCTYPE html>
<html>
<head>
	<meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1""  />
	 <meta http-equiv=""Content-Type"" content=""text/html;charset=utf-8"" />
	 <meta charset=""UTF-8"" /> 
	<title>MediaWiki Test</title>
</head>
<body>
	{0}
</body>
</html>
", WikiToHtml (str));



			System.IO.File.WriteAllText ("/root/sources/mwsyntax.htm", strHTML, System.Text.Encoding.UTF8);
			System.Console.WriteLine (WikiToHtml (str));


			// System.Console.WriteLine (WikiToHtml ("=== bold ==="));
		}


	}


}
