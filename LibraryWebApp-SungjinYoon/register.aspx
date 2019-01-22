<%@ Page Title="" Language="C#" MasterPageFile="~/StdLayout.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="LibraryWebApp_SungjinYoon.register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script lang="javascript">
        function ValidateStudentNo(sender, args) {
            if (args.Value == "-1") {
                alert("In client script");
                args.IsValid = false;

            }
            else
                args.IsValid = true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Name:"></asp:Label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <br />
            <br />
        
            <asp:Label ID="Label2" runat="server" Text="E-mail"></asp:Label>
            :
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <br />
            <br />
            Phone number:
            <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
            <br />
            <br />
            User Category:
            <asp:ListBox ID="lstUserCategory" runat="server" Height="34px"></asp:ListBox>
            <br />
    &nbsp;<br />
            Date of Birth:
            <asp:TextBox ID="txtDateOfBirth" runat="server"></asp:TextBox>
            <asp:Button ID="btnSelDate" runat="server" Text="Select Date" OnClick="btnSelDate_Click" style="height: 40px" />
            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Visible="False" Width="350px" OnSelectionChanged="Calendar1_SelectionChanged">
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                <TodayDayStyle BackColor="#CCCCCC" />
            </asp:Calendar>
            <br />
            <br />
            Password:
            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
            <asp:Button ID="Button2" runat="server" Text="Button" />
        &nbsp;&nbsp;
            <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
        </div>

</asp:Content>
