<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="/">
    <html>
      <head>
        <title>Table of Contents</title>
        <style type="text/css">
          ul.toc { list-style-type: none; }
          ul.toc li { margin-left: 10px; }
        </style>
      </head>
      <body>
        <h1>Table of Contents</h1>
        <ul class="toc">
          <xsl:apply-templates select="toc/section"/>
        </ul>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="section">
    <li>
      <a href="#">
        <xsl:value-of select="@title"/>
      </a>
      <xsl:if test="subsection">
        <ul class="toc">
          <xsl:apply-templates select="subsection"/>
        </ul>
      </xsl:if>
    </li>
  </xsl:template>

  <xsl:template match="subsection">
    <li>
      <a href="#">
        <xsl:value-of select="@title"/>
      </a>
    </li>
  </xsl:template>
</xsl:stylesheet>