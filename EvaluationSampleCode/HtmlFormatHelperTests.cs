using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvaluationSampleCode;
using System.Collections.Generic;

namespace EvaluationSampleCode.Tests
{
  [TestClass]
  public class HtmlFormatHelperTests
  {
    private HtmlFormatHelper _htmlHelper;

    [TestInitialize]
    public void Setup()
    {
      _htmlHelper = new HtmlFormatHelper();
    }

    #region GetBoldFormat Tests
    [TestMethod]
    public void GetBoldFormat_SimpleText_ReturnsBoldFormattedText()
    {
      var content = "Hello World";

      var result = _htmlHelper.GetBoldFormat(content);

      Assert.AreEqual("<b>Hello World</b>", result);
    }

    [TestMethod]
    public void GetBoldFormat_EmptyString_ReturnsBoldTags()
    {
      var content = "";

      var result = _htmlHelper.GetBoldFormat(content);

      Assert.AreEqual("<b></b>", result);
    }

    [TestMethod]
    public void GetBoldFormat_WhitespaceString_ReturnsBoldFormattedWhitespace()
    {
      var content = "   ";

      var result = _htmlHelper.GetBoldFormat(content);

      Assert.AreEqual("<b>   </b>", result);
    }

    [TestMethod]
    public void GetBoldFormat_SpecialCharacters_ReturnsBoldFormattedSpecialChars()
    {
      var content = "!@#$%^&*()";

      var result = _htmlHelper.GetBoldFormat(content);

      Assert.AreEqual("<b>!@#$%^&*()</b>", result);
    }

    [TestMethod]
    public void GetBoldFormat_HtmlContent_ReturnsBoldFormattedHtml()
    {
      var content = "<span>Test</span>";

      var result = _htmlHelper.GetBoldFormat(content);

      Assert.AreEqual("<b><span>Test</span></b>", result);
    }
    #endregion

    #region GetItalicFormat Tests
    [TestMethod]
    public void GetItalicFormat_SimpleText_ReturnsItalicFormattedText()
    {
      var content = "Hello World";

      var result = _htmlHelper.GetItalicFormat(content);

      Assert.AreEqual("<i>Hello World</i>", result);
    }

    [TestMethod]
    public void GetItalicFormat_EmptyString_ReturnsItalicTags()
    {
      var content = "";

      var result = _htmlHelper.GetItalicFormat(content);

      Assert.AreEqual("<i></i>", result);
    }

    [TestMethod]
    public void GetItalicFormat_WhitespaceString_ReturnsItalicFormattedWhitespace()
    {
      var content = "   ";

      var result = _htmlHelper.GetItalicFormat(content);

      Assert.AreEqual("<i>   </i>", result);
    }

    [TestMethod]
    public void GetItalicFormat_SpecialCharacters_ReturnsItalicFormattedSpecialChars()
    {
      var content = "!@#$%^&*()";

      var result = _htmlHelper.GetItalicFormat(content);

      Assert.AreEqual("<i>!@#$%^&*()</i>", result);
    }

    [TestMethod]
    public void GetItalicFormat_HtmlContent_ReturnsItalicFormattedHtml()
    {
      var content = "<span>Test</span>";

      var result = _htmlHelper.GetItalicFormat(content);

      Assert.AreEqual("<i><span>Test</span></i>", result);
    }
    #endregion

    #region GetFormattedListElements Tests
    [TestMethod]
    public void GetFormattedListElements_SingleElement_ReturnsCorrectHtmlList()
    {
      var contents = new List<string> { "Item 1" };

      var result = _htmlHelper.GetFormattedListElements(contents);

      Assert.AreEqual("<ul><li>Item 1</li></ul>", result);
    }

    [TestMethod]
    public void GetFormattedListElements_MultipleElements_ReturnsCorrectHtmlList()
    {
      var contents = new List<string> { "Item 1", "Item 2", "Item 3" };

      var result = _htmlHelper.GetFormattedListElements(contents);

      Assert.AreEqual("<ul><li>Item 1</li><li>Item 2</li><li>Item 3</li></ul>", result);
    }

    [TestMethod]
    public void GetFormattedListElements_EmptyList_ReturnsEmptyHtmlList()
    {
      var contents = new List<string>();

      var result = _htmlHelper.GetFormattedListElements(contents);

      Assert.AreEqual("<ul></ul>", result);
    }

    [TestMethod]
    public void GetFormattedListElements_ElementsWithSpecialCharacters_ReturnsCorrectHtmlList()
    {
      var contents = new List<string> { "<script>", "&amp;", "\"quotes\"" };

      var result = _htmlHelper.GetFormattedListElements(contents);

      Assert.AreEqual("<ul><li><script></li><li>&amp;</li><li>\"quotes\"</li></ul>", result);
    }

    [TestMethod]
    public void GetFormattedListElements_ElementsWithWhitespace_ReturnsCorrectHtmlList()
    {
      var contents = new List<string> { "  Item 1  ", "", "   " };

      var result = _htmlHelper.GetFormattedListElements(contents);

      Assert.AreEqual("<ul><li>  Item 1  </li><li></li><li>   </li></ul>", result);
    }

    [TestMethod]
    public void GetFormattedListElements_LongList_ReturnsCorrectHtmlList()
    {
      var contents = new List<string>();
      for (int i = 1; i <= 10; i++)
      {
        contents.Add($"Item {i}");
      }

      var result = _htmlHelper.GetFormattedListElements(contents);

      var expected = "<ul><li>Item 1</li><li>Item 2</li><li>Item 3</li><li>Item 4</li><li>Item 5</li><li>Item 6</li><li>Item 7</li><li>Item 8</li><li>Item 9</li><li>Item 10</li></ul>";
      Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetFormattedListElements_ElementsWithHtmlContent_ReturnsCorrectHtmlList()
    {
      var contents = new List<string> { "<b>Bold</b>", "<i>Italic</i>", "<span>Span</span>" };

      var result = _htmlHelper.GetFormattedListElements(contents);

      Assert.AreEqual("<ul><li><b>Bold</b></li><li><i>Italic</i></li><li><span>Span</span></li></ul>", result);
    }
    #endregion

    #region Integration Tests
    [TestMethod]
    public void HtmlFormatHelper_CombinedFormats_WorkTogether()
    {
      var boldText = _htmlHelper.GetBoldFormat("Bold Text");
      var italicText = _htmlHelper.GetItalicFormat("Italic Text");
      var listItems = new List<string> { boldText, italicText };

      var result = _htmlHelper.GetFormattedListElements(listItems);

      Assert.AreEqual("<ul><li><b>Bold Text</b></li><li><i>Italic Text</i></li></ul>", result);
    }
    #endregion
  }
}