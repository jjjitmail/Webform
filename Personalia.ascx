<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Personalia.ascx.cs" Inherits="UserControls_Personalia" %>
<table>

<UIControl:ComboBox DataBinding = "CurrentVersion" DefaultWaarde ="0"   
        ItemsField ="HistoryChangesList" runat ="server" ID ="P_HistoryChangesList" 
        LabelTekst ="Versie" onselectedindexchanged="P_HistoryChangesList_SelectedIndexChanged" AutoPostBack="true" />
    <UIControl:LabelTekstVeld DataBinding="Roepnaam" runat ="server" ID="TB_Roepnaam" LabelTekst="Roepnaam" />
    <UIControl:LabelTekstVeld DataBinding="VoorNamen" runat ="server" ID="TB_Voornamen" LabelTekst="Voornamen" />
    <UIControl:LabelTekstVeld DataBinding="Voorletters" runat ="server" ID="TB_Voorletters" LabelTekst="Voorletters" />
    <UIControl:LabelTekstVeld DataBinding="Tussenvoegsel" runat ="server" ID="TB_Tussenvoegsel" LabelTekst="Tussenvoegsel" />
    <UIControl:LabelTekstVeld DataBinding="AchterNaam" runat ="server" ID="TB_Achternaam" LabelTekst="Achternaam" />
    <UIControl:LabelTekstVeld DataBinding="Voorkeursnaam" runat ="server" ID="TB_Voorkeursnaam" LabelTekst="Voorkeursnaam" />
    <UIControl:LabelTekstVeld DataBinding="Geboortenaam" runat ="server" ID="TB_Geboortenaam" LabelTekst="Geboortenaam" />
   <%-- <tr>
        <td valign="top"><asp:Label runat ="server" EnableViewState="false" ID ="lbl_PasFoto" Text ="Pas Foto" /></td>
        <td>
            <div><asp:FileUpload EnableViewState="false" ID="xxxFU_PasFoto" runat="server" CssClass="ButtonStyle"  /></div>
            <div>
                <asp:Image ID="Im_PasFoto" EnableViewState="false" runat="server" ImageUrl="~/Images/Leeg.gif" />
                <asp:Button runat="server" ID="lbtnDelete" CssClass="ButtonStyleDeleteSmall" Text="" onclick="lbtnDeleteFoto_Click" />
            </div>
        </td>
    </tr>--%>
  <UIControl:ImageUploadVeld DataBinding="Foto" runat ="server" ID="FU_PasFoto" ImageUrl="~/Handler/ImageHandler.ashx?pid=0" ImageId="Pid" 
        CssClass="ButtonStyleDeleteSmall" LabelTekst="Pas Foto" OnClick="lbtnDeleteFoto_Click" />
    
    <UIControl:LabelTekstVeld DataBinding="Geboorteplaats" runat ="server" ID="TB_Geboorteplaats" LabelTekst="Geboorteplaats" />
    <UIControl:ComboBox DataBinding = "Geboorteland" DefaultWaarde ="144" ItemsField ="GeboortelandList" runat ="server" ID ="P_LandenLijst" LabelTekst ="Geboorteland" />
    <UIControl:CalendarVeld DataBinding = "Geboortedatum" runat="server" ID="TB_Geboortedatum" LabelTekst="Geboorte Datum" ImageUrl="~/images/CalendarIcon.gif"/>
    <UIControl:ComboBox DataBinding = "Geslacht" ItemsField ="GeslachtList" DefaultWaarde ="0" runat ="server" ID ="DD_Geslacht" LabelTekst ="Geslacht" />
    <UIControl:LabelTekstVeld DataBinding="Titels" runat ="server" ID="TB_Titles" LabelTekst="Titels" />
    <UIControl:LabelTekstVeld DataBinding="Bsn" runat ="server" ID="TB_BSN" LabelTekst="BSN" />
    <UIControl:ComboBox DataBinding = "Nationaliteit" DefaultWaarde ="138" ItemsField ="NationaliteitList" runat ="server" ID ="P_LandenLijst_Nationaliteit" LabelTekst ="Nationaliteit" />
    <UIControl:ComboBox DataBinding = "Identiteitsbewijs" DefaultWaarde ="0" ItemsField ="IdentiteitsbewijsList" runat ="server" ID ="DD_Identiteitsbewijs" LabelTekst ="Identiteitsbewijs" />
    <UIControl:LabelTekstVeld DataBinding="Identiteitsbewijsnr" runat ="server" ID="TB_Identiteitsbewijsnr" LabelTekst="Identiteitsbewijs nummer" />    
    <UIControl:LabelTekstVeld DataBinding="Rijbewijsnr" runat ="server" ID="TB_Rijbewijsnr" LabelTekst="Rijbewijsnr" />
    <UIControl:LabelTekstVeld DataBinding="Rijbewijstype" runat ="server" ID="TB_Rijbewijstype" LabelTekst="Soort Rijbewijs" /> 
    <UIControl:CalendarVeld DataBinding = "Datum_Overlijden" runat="server" ID="TB_Datum_Overlijden" LabelTekst="Datum Overlijden" ImageUrl="~/images/CalendarIcon.gif"/>
    <UIControl:LabelCheckBox DataBinding="Mailing_Prive" runat ="server" ID="TB_Mailing_Prive" LabelTekst="Mailing Prive" />
    <UIControl:LabelCheckBox DataBinding="Mailing_Omegam" runat ="server" ID="TB_Mailing_Omegam" LabelTekst="Mailing Omegam" />
    
    <tr><td colspan="2" style=" height:200px;">
         
         </td></tr>
     
</table>

<%--<tr>
        <td valign="top"><asp:Label runat ="server" EnableViewState="false" ID ="lbl_PasFoto" Text ="Pas Foto" /></td>
        <td>
            <div><asp:FileUpload EnableViewState="false" ID="xxxFU_PasFoto" runat="server" CssClass="ButtonStyle"  /></div>
            <div>
                <asp:Image ID="Im_PasFoto" EnableViewState="false" runat="server" ImageUrl="~/Images/Leeg.gif" />
                <asp:Button runat="server" ID="lbtnDelete" CssClass="ButtonStyleDeleteSmall" Text="" onclick="lbtnDeleteFoto_Click" />
            </div>
        </td>
    </tr>--%>