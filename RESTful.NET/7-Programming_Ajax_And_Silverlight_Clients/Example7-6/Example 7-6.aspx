<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Using WCF Service from ASP.NET AJAX</title>
</head>
<body onload="getDomains()">
    <form runat="server">
        <asp:ScriptManager runat="server" id="_scriptMan">
            <Services>
                <asp:ServiceReference path="~/BioWrapperService" />
            </Services>
        </asp:ScriptManager>
        <h1>Life classification</h1>
        <p>
        Domain:<select id="domains" onchange="selectDomain(this)"></select>
        </p>
        <p>
        Kingdom:<select id="kingdoms"></select>
        </p>
    </form>
</body>
</html>
