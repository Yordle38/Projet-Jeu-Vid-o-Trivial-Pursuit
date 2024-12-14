<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:tp="http://www.univ-grenoble-alpes.fr/l3miage/trivialpursuit">

    <xsl:output method="html" indent="yes" encoding="UTF-8"/>

    <!--Paramètre pour le theme a afficher -->
    <xsl:param name="theme" select="'Zythologie'"/>
    
    <xsl:template match="/tp:Categories">
        <html>
            <head>
                <title>Questions du thème : <xsl:value-of select="$theme"/></title>
                <style>
                    body { font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    padding: 20px; }
                    .question { border: 1px solid #ddd;
                    border-radius: 5px;
                    background-color: #fff;
                    margin-bottom: 20px;
                    padding: 15px;
                    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1); }
                    .question .header { font-weight: bold;
                    color: #333;
                    margin-bottom: 10px; }
                </style>
            </head>
            <body>
                <h1>Questions du thème : <xsl:value-of select="$theme"/></h1>
                <xsl:apply-templates select="tp:Categorie[@nom=$theme]/tp:Cartes/tp:Carte"/>
            </body>
        </html>
    </xsl:template>

    <xsl:template match="tp:Carte">
        <div class="question">
            <div class="header">
                Difficulté : <xsl:value-of select="@difficulte"/>
            </div>
            <div class="content">
                <p><strong>Question :</strong> <xsl:value-of select="tp:Question"/></p>
                <h4>Réponses :</h4>
                <ul>
                    <xsl:for-each select="tp:Reponses/tp:Reponse">
                        <li>
                            <xsl:value-of select="@texte"/>
                            <xsl:if test="@correct='true'">
                                <strong> (Correct)</strong>
                            </xsl:if>
                        </li>
                    </xsl:for-each>
                </ul>
            </div>
        </div>
    </xsl:template>
</xsl:stylesheet>
