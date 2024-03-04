<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SRF_Warehouse.aspx.cs" Inherits="REPI_PUR_SOFRA.SRF_Warehouse" MasterPageFile="~/Sofra.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 

    <link href="plugins/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="plugins/node-waves/waves.css" rel="stylesheet" type="text/css" />
    <link href="plugins/animate-css/animate.css" rel="stylesheet" type="text/css" />
    <link href="plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/themes/all-themes.css" rel="stylesheet" type="text/css" />
    <link href="plugins/sweetalert/sweetalert.css" rel="stylesheet" type="text/css" /> 
    
    <style type="text/css">
        .pagination-ys 
        {
          padding-left: 0;
          margin: 20px 0;
          border-radius: 4px;         
        }  
        .pagination-ys table > tbody > tr > td 
        {
          display: inline;         
        }  
        .pagination-ys table > tbody > tr > td > a, .pagination-ys table > tbody > tr > td > span 
        {
          position: relative;
          float: left;
          padding: 3px 5px;
          line-height: 1.42857143;
          text-decoration: none;
          color: white;
          background-color: #00BCD4;
          border: 1px solid #dddddd;
          margin-left: -1px;         
        }  
        .pagination-ys table > tbody > tr > td > span 
        {
          position: relative;
          float: left;
          padding: 3px 5px;
          line-height: 1.42857143;
          text-decoration: none;         
          margin-left: -1px;
          z-index: 2;
          color: white;
          background-color: #FF5722;
          border-color: white;
          cursor: default;         
        }  
        .pagination-ys table > tbody > tr > td:first-child > a, .pagination-ys table > tbody > tr > td:first-child > span 
        {
          margin-left: 0;
          border-bottom-left-radius: 4px;
          border-top-left-radius: 4px;        
        }  
        .pagination-ys table > tbody > tr > td:last-child > a, .pagination-ys table > tbody > tr > td:last-child > span 
        {
          border-bottom-right-radius: 4px;
          border-top-right-radius: 4px;         
        }  
        .pagination-ys table > tbody > tr > td > a:hover, .pagination-ys table > tbody > tr > td > span:hover, .pagination-ys table > tbody > tr > td > a:focus, .pagination-ys table > tbody > tr > td > span:focus 
        {
          color: #97310e;
          background-color: #eeeeee;
          border-color: #dddddd;
        } 
    </style>
    
    <style type="text/css">
        /* Popup box BEGIN */
        .hover_bkgr_fricc{
            background:rgba(0,0,0,.4);
            cursor:pointer;
            display:none;
            height:100%;
            position:fixed;
            text-align:center;
            top:0;
            width:100%;
            left:0px;
        }
        .hover_bkgr_fricc .helper{
            display:inline-block;
            height:100%;
            vertical-align:middle;
        }
        .hover_bkgr_fricc > div {
            background-color: #fff;
            display: inline-block;
            height: auto;
            min-width: 1070px;
            min-height: 100px;
            vertical-align: middle;
            position: relative;
            border-radius: 8px;
            padding: 15px 15px;
            margin-top: 100px;
        }
        .popupCloseButton {
            background-color: #fff;
            border: 3px solid #999;
            cursor: pointer;
            display: inline-block;
            font-family: arial;
            font-weight: bold;
            position: absolute;
            top: -20px;
            right: -20px;
            font-size: 25px;
            line-height: 30px;
            width: 30px;
            height: 30px;
            text-align: center;
        }
        .popupCloseButton:hover {
            background-color: #ccc;
        }
        .trigger_popup_fricc {
            cursor: pointer;
            font-size: 20px;
            margin: 20px;
            display: inline-block;
            font-weight: bold;
        }
        
        .textHeight 
        {
        	min-height:20px;
        	verflow: hidden;
        }
        /* Popup box BEGIN */
    </style>
    
    <style type="text/css">
        /* Popup box BEGIN */
        .hover_bkgr_fricc2{
            background:rgba(0,0,0,.4);
            cursor:pointer;
            display:none;
            height:100%;
            position:fixed;
            text-align:center;
            top:0;
            width:100%;
            left:0px;
        }
        .hover_bkgr_fricc2 .helper{
            display:inline-block;
            height:100%;
            vertical-align:middle;
        }
        .hover_bkgr_fricc2 > div {
            background-color: #fff;
            display: inline-block;
            height: auto;
            width: 1200px;
            min-height: 100px;
            vertical-align: middle;
            position: relative;
            border-radius: 8px;
            padding: 15px 15px;
            margin-top: 100px;
        }
        .popupCloseButton2 {
            background-color: #fff;
            border: 3px solid #999;
            cursor: pointer;
            display: inline-block;
            font-family: arial;
            font-weight: bold;
            position: absolute;
            top: -20px;
            right: -20px;
            font-size: 25px;
            line-height: 30px;
            width: 30px;
            height: 30px;
            text-align: center;
        }
        .popupCloseButton2:hover {
            background-color: #ccc;
        }
        .trigger_popup_fricc2 {
            cursor: pointer;
            font-size: 20px;
            margin: 20px;
            display: inline-block;
            font-weight: bold;
        }
        
        /* Popup box BEGIN */
    </style>
    
    <style type="text/css">
        /* Popup box BEGIN */
        .hover_bkgr_fricc3{
            background:rgba(0,0,0,.4);
            cursor:pointer;
            display:none;
            height:100%;
            position:fixed;
            text-align:center;
            top:0;
            width:100%;
            left:0px;
        }
        .hover_bkgr_fricc3 .helper{
            display:inline-block;
            height:100%;
            vertical-align:middle;
        }
        .hover_bkgr_fricc3 > div {
            background-color: #fff;
            display: inline-block;
            height: auto;
            width: 1300px;
            min-height: 100px;
            vertical-align: middle;
            position: relative;
            border-radius: 8px;
            padding: 15px 15px;
            margin-top: 250px;
        }
        .popupCloseButton3 {
            background-color: #fff;
            border: 3px solid #999;
            cursor: pointer;
            display: inline-block;
            font-family: arial;
            font-weight: bold;
            position: absolute;
            top: -20px;
            right: -20px;
            font-size: 25px;
            line-height: 30px;
            width: 30px;
            height: 30px;
            text-align: center;
        }
        .popupCloseButton3:hover {
            background-color: #ccc;
        }
        .trigger_popup_fricc3 {
            cursor: pointer;
            font-size: 20px;
            margin: 20px;
            display: inline-block;
            font-weight: bold;
        }
        
        /* Popup box BEGIN */
    </style>
    
    <style type="text/css">
        .WordWrap
        {
            width: 100%;
            word-break: break-all;
        }
        .WordBreak
        {
            width: 100px;
            overflow: hidden;
            text-overflow: ellipsis;
        }
    </style>
    


    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>   
    <script src="plugins/sweetalert/sweetalert.min.js" type="text/javascript"></script>
    <script src="js/pages/ui/tooltips-popovers.js" type="text/javascript"></script>  
    
    <script type="text/javascript">
        function SuccessMessage(msg) {
            swal({
                title: "SUCCESS",
                text: msg,
                type: "success"
            });
        }
        
        function ErrorMessage(msg) {
            swal({
                title: 'ERROR!',
                text: msg,
                type: 'warning'
            });
        }
        
               
        
    </script>
    
    <script type="text/javascript">
        $(function () {
            $("[id*=txtFrom]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            }).on('changeDate', function (e) {
                $(this).datepicker('hide')
            });
            
        });
        
        $(function () {
            $("[id*=txtTo]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            }).on('changeDate', function (e) {
                $(this).datepicker('hide')
            });
            
        });
    </script>
    
     <script type="text/javascript">
        function showDialog()
        {
            $('.hover_bkgr_fricc').show();
        }
        
        function hideDialog()
        {
            $('.hover_bkgr_fricc').hide();
        }
        
        function showDialog2()
        {
            $('.hover_bkgr_fricc2').show();
        }
        
        function hideDialog2()
        {
            $('.hover_bkgr_fricc2').hide();
        }
        
        function showDialog3()
        {
            $('.hover_bkgr_fricc3').show();
        }
        
        function hideDialog3()
        {
            $('.hover_bkgr_fricc3').hide();
        }
        
    </script>
    
    <script type="text/javascript">
        function AddRow() {
            
            try {
                    
                    if (document.getElementById('<%= ddActualItemName.ClientID %>').value == '')
                    {
                        alert('Please select a valid Item Name');
                        return false;
                    } else if (document.getElementById('<%= txtActualQuantity2.ClientID %>').value == '')
                    {
                        alert('Please enter a valid Actual Quantity');
                        return false;
                    } else if (document.getElementById('<%= ddWithDocuments.ClientID %>').value == '')
                    {
                        alert('Please select With Documents?');
                        return false;
                    }                     
                    else
                    { 
                        var actualDropDown = document.getElementById('<%= ddActualItemName.ClientID %>');
                        var selectedText = actualDropDown.options[actualDropDown.selectedIndex].value;
                        var orderQuantity = selectedText.substring(selectedText.indexOf("(") + 1,selectedText.lastIndexOf(")"))
                        var actualQuantity = document.getElementById('<%= txtActualQuantity2.ClientID %>').value;
                        
                        
                        if (parseInt(actualQuantity, 10) > parseInt(orderQuantity, 10))
                        {
                            alert('Actual total delivered quantity must be equal to actual ordered quantity or Actual total delivered quantity is less than to the actual ordered quantiy if partially delivered!');
                            //document.getElementById('<%= pError.ClientID %>').innerText = 'Actual total delivered quantity must be equal to actual ordered quantity or Actual total delivered quantity is less than to the actual ordered quantiy if partially delivered!';
                            return false;
                        } else {
                        
                                var gridView = document.getElementById('<%= gvActualDelivery.ClientID %>');
                             
                                //Reference the TBODY tag.
                                var tbody = gridView.getElementsByTagName("tbody")[0];
                             
                                //Reference the first row.
                                var row = tbody.getElementsByTagName("tr")[1];
                             
                                //Check if row is dummy, if yes then remove.
                                if (row.getElementsByTagName("td")[0].innerHTML.replace(/\s/g, '') == "") {
                                    tbody.removeChild(row);
                                }
                             
                                //Clone the reference first row.
                                row = row.cloneNode(true);
                                
                                var ddActualRefId = document.getElementById('<%= ddActualItemName.ClientID %>');
                                SetValueForRefId(row, 0, "Warehouse_DetailsRefId", ddActualRefId);      
                                
                                var ddActualItemName = document.getElementById('<%= ddActualItemName.ClientID %>');                                           
                                SetValueForItemName(row, 1, "Warehouse_ItemName", ddActualItemName);
                                
                                         
                                var txtActualQuantity2 = document.getElementById('<%= txtActualQuantity2.ClientID %>');
                                SetValue(row, 2, "Warehouse_TotalActualQuantity", txtActualQuantity2);
                                
                                
                                var date = new Date();
	                            var curr_date = date.getMonth()+1 + "/" + date.getDate() + "/" + date.getFullYear() + " " + date.getHours()+":"+date.getMinutes()+":"+ date.getSeconds();
	                            
	                            //alert(date.getMonth() + "/" + date.getDate() + "/" + date.getFullYear());
	
                                SetValueForNewEntry(row, 3, "Warehouse_DeliveredDate", curr_date);
                                
                                SetValueForNewEntry(row, 4, "Warehouse_NewEntry", "YES");
                                
                                SetValueForNewEntry(row, 5, "Warehouse_WithDocuments", document.getElementById('<%= selectedWithDocuments.ClientID %>').value);
                                
                                
                                //REMOVE ITEM FROM DROPDOWNLIST AFTER THE INSERT       
                                document.getElementById('<%= ddActualItemName.ClientID %>').remove(document.getElementById('<%= selectedItemName.ClientID %>').value);
                                

                             
                                //Add the row to the GridView.
                                tbody.appendChild(row);
                                return false;
                        
                        }
                        
                        
                    }
                }
                catch(err) {
                  alert(err);
                  return false;
                }
        }
        
        function SetValueForNewEntry(row, index, name, textbox) {
            
            row.cells[index].innerHTML = textbox;       
            //Create and add a Hidden Field to send value to server.
            var input = document.createElement("input");
            input.type = "hidden";
            input.name = name;
            input.value = textbox;
            row.cells[index].appendChild(input);

        }
        
        
        function SetValueForItemName(row, index, name, textbox) {
            //Reference the Cell and set the value.
            row.cells[index].innerHTML = textbox.options[document.getElementById('<%= selectedItemName.ClientID %>').value].innerHTML.substring(textbox.options[document.getElementById('<%= selectedItemName.ClientID %>').value].innerHTML.indexOf("]") + 1, textbox.options[document.getElementById('<%= selectedItemName.ClientID %>').value].innerHTML.Length);
            //row.cells[index].innerHTML = textbox.options[document.getElementById('<%= selectedItemName.ClientID %>').value].innerHTML.substring(0, document.getElementById('<%= selectedItemName.ClientID %>').value.indexOf("]"));
            //row.cells[index].innerHTML = textbox.options[textbox.selectedIndex].value.substring(0,textbox.options[textbox.selectedIndex].value.indexOf("("));       
            //Create and add a Hidden Field to send value to server.
            var input = document.createElement("input");
            input.type = "hidden";
            input.name = name;
            //input.value = textbox.options[document.getElementById('<%= selectedItemName.ClientID %>').value].innerHTML.substring(textbox.options[document.getElementById('<%= selectedItemName.ClientID %>').value].innerHTML.indexOf("]") + 1, textbox.options[document.getElementById('<%= selectedItemName.ClientID %>').value].innerHTML.Length);
            input.value = textbox.value;
            row.cells[index].appendChild(input);
         
            //alert(textbox.options[document.getElementById('<%= selectedItemName.ClientID %>').value].innerHTML.substring(textbox.options[document.getElementById('<%= selectedItemName.ClientID %>').value].innerHTML.indexOf("]") + 1, textbox.options[document.getElementById('<%= selectedItemName.ClientID %>').value].innerHTML.Length));
            //Clear the TextBox.
            textbox.value = "";
        }
        
        function getSelectedItemName(val)
        {
            document.getElementById('<%= selectedItemName.ClientID %>').value = val.selectedIndex;
        }
        
        function getSelectedWithDocuments(val)
        {
            document.getElementById('<%= selectedWithDocuments.ClientID %>').value = val.options[val.selectedIndex].innerHTML;
        }
        
//        function setAITD(val)
//        {
//            if (val.checked)
//            {
//                document.getElementById('<%= checkIfSelected.ClientID %>').value = 
//                alert('checked');
//            } else {
//                alert('not checked');
//            }
//        }
        
//        function onShowClick() {
//            document.getElementById('<%= checkIfSelected.ClientID %>').value = '';
//            var dataGrid = document.getElementById('<%= gvDetails.ClientID %>');
//            var rows = dataGrid.rows;
//            for (var index = 1; index < rows.length; index++) {
//                var checkBoxes = rows[index].cells[0].getElementsByTagName('INPUT');
//                for (i = 0; i < checkBoxes.length; i++) {
//                    if (checkBoxes[i].type == 'checkbox') {
//                    
//                        if (checkBoxes[i] != undefined && checkBoxes[i].checked) {
//                            
//							document.getElementById('<%= checkIfSelected.ClientID %>').value += "yes,";
//							alert('checked');
//                        }
//                    }
//                }
//            }
//            
//            if (document.getElementById('<%= checkIfSelected.ClientID %>').value == '')
//            {
//                alert('Please check AITD or ACTUAL ITEMS TO DELIVER.');
//                return false;
//            } else {
//                document.getElementById('<%= btnInProgress.ClientID %>').click;
//            }            
//        }
         
        function SetValue(row, index, name, textbox) {
            //Reference the Cell and set the value.
            row.cells[index].innerHTML = textbox.value;
                   
            //Create and add a Hidden Field to send value to server.
            var input = document.createElement("input");
            input.type = "hidden";
            input.name = name;
            input.value = textbox.value;
            row.cells[index].appendChild(input);
         
            //Clear the TextBox.
            textbox.value = "";
        }
        
        function SetValueForRefId(row, index, name, textbox) {
            //Reference the Cell and set the value.
            row.cells[index].innerHTML = textbox.options[textbox.selectedIndex].value.substring(0,textbox.options[textbox.selectedIndex].value.indexOf("("));
            
            //Create and add a Hidden Field to send value to server.
            var input = document.createElement("input");
            input.type = "hidden";
            input.name = name;
            input.value = textbox.options[textbox.selectedIndex].value.substring(0,textbox.options[textbox.selectedIndex].value.indexOf("("));
            
            
                        
            row.cells[index].appendChild(input);
         
            //Clear the TextBox.
            textbox.value = "";
        }
        
        function isNumberKey(evt)
        {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

             return true;
        }
        
        function ManipulateGrid() {
            
            try
            {
                var gvDrv = document.getElementById('<%= gvActualDelivery.ClientID %>');
                var query = '';
                var lblCtrlNo2 = document.getElementById('<%= lblCtrlNo2.ClientID %>');
                
                for (var i = 1; i < gvDrv.rows.length; i++) 
                {
                
                    var cell = gvDrv.rows[i].cells;
                    var refid = cell[0].innerText;
                    var actualqty = cell[2].innerText;
                    var deliveredDate = cell[3].innerText;
                    var newEntry = cell[4].innerText;
                    var withDocs = cell[5].innerText;
                    
                    var doc = '';
                    
                        if (newEntry == 'YES')
                        {
                            if (withDocs == 'YES')
                            {
                                doc = '1';
                            }
                            if (withDocs == 'NO')
                            {
                                doc = '0';
                            }
                            
                            query += "INSERT INTO SRF_TRANSACTION_Warehouse_Actual_Delivery (DetailsRefId,SRFNumber,Quantity,ActualQuantity,AddedBy,AddedDate,WithDocuments) ";
                            query += "VALUES ('" + refid + "','" + lblCtrlNo2.innerHTML + "','0','" + actualqty + "','" + document.getElementById('<%= hiddenUserId.ClientID %>').value + "','" + deliveredDate + "','" + doc + "') ";
                            
                            
//                            query += "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
//                                          "VALUES ('" + lblCtrlNo2.innerHTML + "','" + document.getElementById('<%= hiddenUserId.ClientID %>').value + "', GETDATE(), '2', '" + attFile + "') ";
//                                          
//                            selectedAtt += attFile + ",";
//                            
//                            alert(cell[5].files[i].innerText);  
                           
                        }
                }
                
                document.getElementById('<%= csvActualCollections.ClientID %>').value = query;
                //document.getElementById('<%= csvActualCollections2.ClientID %>').value = selectedAtt;
                //alert(document.getElementById('<%= csvActualCollections.ClientID %>').value);
            
            }
            catch(err) {
              alert(err);
            }
        }
        
        function validateAtt(input)
        {
            var file = input.files[0];
            
            alert(input.value);
        }
        

    </script>

    
        
    
<asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   
        <section class="content">
        <div class="container-fluid" style="margin-top:-50px; margin-left:-320px; width:1800px;">
                                                  
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1800px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">SERVICE REPAIR WAREHOUSE RECEIVING ENTRY</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1800px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:100px; width:1650px;">
                            <div style="margin-top:10px;">
                                <table style="width:1650px; color:Gray; font-size:12px;">
                                  <tr>
                                    <th>ENTER ITEM YOU WANT TO SEARCH (Control Number or 8106 Number)</th>
                                    <th style="color:White;">DUMMY</th>
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtSearch" runat="server" Width="680px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" onkeydown='searchWithKeyPress(this)' /></td>
                                    <td>
                                        <asp:DropDownList ID="ddStatus" runat="server" Width="320px" Height="28px" class="form-control">
                                            <asp:ListItem Text="" Value="" />
                                            <asp:ListItem Text="APPROVED / WAITING FOR DELIVERY" Value="APPROVED / WAITING FOR DELIVERY" />
                                            <asp:ListItem Text="DELIVERY IN-PROGRESS" Value="DELIVERY IN-PROGRESS" />
                                            <asp:ListItem Text="DELIVERED WITH PENDING ITEMS" Value="DELIVERED WITH PENDING ITEMS" />
                                            <asp:ListItem Text="DELIVERED" Value="DELIVERED" />
                                            <asp:ListItem Text="CLOSED" Value="CLOSED" />
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIncludeDelivered" runat="server" Text="INCLUDE DELIVERED & CLOSED ITEMS" Checked="false"  />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="SEARCH" Height="28px" CssClass="btn bg-green waves-effect" OnClick="btnSubmit_Click" />
                                    </td>   
                                    <td><asp:Button ID="btnDownloadExcel" runat="server" Text="DOWNLOAD EXCEL REPORT" Height="28px"  OnClientClick="showDialog3(); return false;" CssClass="btn bg-light-blue waves-effect" /></td>                         
                                  </tr>
                                </table>
                            </div>
                        </div>                        
                    </div>
                </div>
            </div>   
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1800px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; min-height:100px; width:1800px;">
                            
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     PagerSettings-Mode="NumericFirstLast" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvData_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!">

                                <Columns>
                                    <asp:TemplateField HeaderText="CTRLNo" HeaderStyle-Width="170px" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblCtrl" runat="server" Font-Bold="true" Visible="false" Text='<%# Eval("Warehouse_CtrlNo") %>' CommandName="lblCtrl_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                            
                                            <asp:Label ID="lblCTRLNo" runat="server" Width="170px" Text='<%# Eval("Warehouse_CtrlNo") %>'  /> 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="8106/8112 NO." HeaderStyle-Width="170px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblLOA8106" runat="server" Width="170px" Text='<%# Eval("Warehouse_LOA8106") %>'  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                 <Columns>
                                    <asp:TemplateField HeaderText="8105 NO." HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lbl8105" runat="server" Width="200px" Text='<%# Eval("Warehouse_8105") %>'  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                 <Columns>
                                    <asp:TemplateField HeaderText="CATEGORY" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Width="200px" Text='<%# Eval("CategoryDescription") %>'  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REQUESTER" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequester" runat="server" Width="300px" Text='<%# Eval("Warehouse_RequesterName") %>'  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SUPPLIER" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplier" runat="server" Width="300px" Text='<%# Eval("Warehouse_SupplierName") %>'  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="STATUS" HeaderStyle-Width="190px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Width="190px" Text='<%# Eval("StatAll") %>' ForeColor="White" Font-Bold="true" />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbPrint" runat="server" Text="VIEW" CommandName="lbPrint_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />                                            
                                            <asp:Label ID="lblLOACount2" runat="server" Visible="false" Text='<%# Eval("Warehouse_LoaCount2") %>'  />  
                                        </ItemTemplate>                                                                        
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="PENDING DOCUMENTS?" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbPendingDocuments" runat="server" CommandName="lbPendingDocuments_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" /> 
                                        </ItemTemplate>                                                                        
                                    </asp:TemplateField>
                                </Columns>
                                
                            </asp:GridView>
                            
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="hover_bkgr_fricc" style="overflow:auto;">
                <span class="helper"></span>
                <div>
                    
                    
                     <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1200px;">
                            <div class="card">
                                <div class="body" style="margin-top:0px; min-height:20px; width:1060px;">
                                    <table style="width:1120px; color:Black; text-align:center;">
                                        <tr>
                                            <td style="font-size:18px;"><asp:Label ID="lblCtrlNo2" runat="server" Font-Bold="true" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                     </div>
                    
                    
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1200px;">
                            <div class="card">
                                <div class="body" style="margin-top:-23px; min-height:80px; width:1200px;">
                                    <table style="width:100%; color:Gray;">
                                      <tr>
                                        <th>REQUESTOR</th>
                                        <th>MANAGER</th> 
                                        <th>INCHARGE</th> 
                                        <th>SCD DEPT. MANAGER</th> 
                                        <th>IMPEX</th> 
                                        
                                      </tr>
                                      <tr>
                                        <td>
                                            <table style="width:180px; color:Gray; text-align:left;">
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblRequestor" runat="server" Font-Bold="true" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblDOARequestor" runat="server" /></td>
                                                </tr>
                                            </table>
                                        </td> 
                                        <td>
                                            <table style="width:180px; color:Gray; text-align:left;">
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblManager" runat="server" Font-Bold="true" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblDOAManager" runat="server" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table style="width:180px; color:Gray; text-align:left;">
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblIncharge" runat="server" Font-Bold="true" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblDOAIncharge" runat="server" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table style="width:180px; color:Gray; text-align:left;">
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblSCDDeptManager" runat="server" Font-Bold="true" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblDOASCDDeptManager" runat="server" Text="-" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table style="width:180px; color:Gray; text-align:left;">
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblImpex" runat="server" Font-Bold="true" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblDOAImpex" runat="server" /></td>
                                                </tr>
                                            </table>
                                        </td>  
                                                                     
                                      </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1200px;">
                            <div class="card"> 
                                <div class="body" style="margin-top:-23px; height:290px; width:1200px;">
                                    <table style="width:100%; color:Gray; text-align:left;">
                                      <tr>
                                        <th>SUPPLIER</th>                                
                                      </tr>
                                      <tr>
                                        <td><asp:Label ID="lblSupplier" runat="server" /></td>
                                      </tr>                              
                                    </table>
                                    <table style="width:100%; color:Gray; text-align:left;">
                                      <tr>                                
                                        <th>REQUESTER</th> 
                                        <th>LOCAL NUMBER</th>
                                        <th>DIVISION</th> 
                                        <th>DEPARTMENT</th>                                 
                                      </tr>
                                      <tr>                                                               
                                        <td><asp:TextBox ID="txtRequester" runat="server" Width="250px" Height="22px" /></td>
                                        <td><asp:TextBox ID="txtLocalNumber" runat="server" Width="250px" Height="22px" /></td> 
                                        <td><asp:TextBox ID="txtDivisionName" runat="server" Width="250px" Height="22px" /></td>
                                        <td><asp:TextBox ID="txtDepartment" runat="server" Width="250px" Height="22px" /></td>                                 
                                      </tr>
                                    </table>
                                    <table style="width:100%; margin-top:10px; color:Gray; text-align:left;">
                                      <tr>
                                        <th>DELIVERY DATE TO REPI</th>
                                        <th>PROBLEM ENCOUNTERED</th>
                                        <th>PURPOSE OF PULL OUT</th> 
                                        <th>TOTAL VALUE (USD)</th>                                
                                      </tr>
                                      <tr>
                                        <td><asp:TextBox ID="txtDeliveryDateToRepi" runat="server" Width="250px" Height="22px" /></td>
                                        <td><asp:TextBox ID="txtProblemEncountered" runat="server" Width="250px" Height="22px" /></td>
                                        <td><asp:TextBox ID="txtPurposeOfPullOut" runat="server" Width="250px" Height="22px" /></td> 
                                        <td><asp:TextBox ID="txtTotalValueInUsd" runat="server" Width="250px" Height="22px" /></td>                                
                                      </tr>
                                    </table>
                                    <table style="width:100%; margin-top:10px; color:Gray; text-align:left;">
                                      <tr>
                                        <th>LOA NO.</th>
                                        <th>SURETY BOND NO.</th>
                                        <th>LOA 8106 NO.</th> 
                                        <th>CATEGORY</th>                                
                                      </tr>
                                      <tr>
                                        <td><asp:TextBox ID="txtLoaNumber" runat="server" Width="250px" Height="22px" /></td> 
                                        <td><asp:TextBox ID="txtSuretyBond" runat="server" Width="250px" Enabled="false" Height="22px" /></td>   
                                        <td><asp:TextBox ID="txtLoa8106" runat="server" Width="250px" Enabled="false" Height="22px" /></td>
                                        <td><asp:TextBox ID="txtCategory" runat="server" Width="250px" Height="22px" /></td>                         
                                      </tr>
                                    </table>     
                                    <table style="width:100%; margin-top:10px; color:Gray; text-align:left;">
                                      <tr>
                                        <th>PICKUP POINT</th> 
                                        <!--<th>GATE PASS NO.</th> -->
                                        <th>TOTAL QUANTITY</th>
                                        <th>PULLOUT DATE</th>  
                                        <th>&nbsp;</th>                              
                                      </tr>
                                      <tr>
                                        <td><asp:TextBox ID="txtPickUpPoint" runat="server" Width="250px" Height="22px" /></td>
                                        <!--<td><asp:TextBox ID="txtGatePassNo" runat="server" Width="200px" Height="22px" /></td>-->
                                        <td><asp:TextBox ID="txtTotalQuantity" runat="server" Width="250px" Height="22px" /></td>
                                        <td><asp:TextBox ID="txtServiceDate" runat="server" Width="250px" Height="22px" /></td> 
                                        <td><asp:TextBox ID="txtDummy" runat="server" Width="250px" Height="22px" BorderStyle="None" /></td>
                                      </tr>
                                    </table>                                                                           
                                    <table style="width:100%; margin-top:10px; color:Gray; display:none;">
                                      <tr>
                                        <th>REMARKS</th>                                
                                      </tr>
                                      <tr>                                                                                                
                                        <td><asp:TextBox ID="txtRemarks" runat="server" Width="977px" Height="22px" /></td>                              
                                      </tr>
                                    </table>
                                    
                                </div>                                                     
                                                       
                            </div>                                         
                            
                        </div>
                    </div>  
                    
                    
                    
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1200px;">
                            <div class="card">
                                <div class="body" style="margin-top:-23px; min-height:100px; width:1200px; text-align:left;">
                                
                                <div class="WordWrap">
                                <asp:GridView ID="gvDetails" runat="server"
                                                          AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" OnRowDataBound="gvDetails_OnRowDataBound"
                                                          HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" FooterStyle-Font-Size="10px">
                                            <Columns>                                   
                                            <asp:TemplateField HeaderText="REFID" ItemStyle-CssClass="columnSpace">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRefId" runat="server" Width="40px" Text='<%# Eval("RefId") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REF PR/PO" ItemStyle-CssClass="columnSpace">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtRefPRPO" runat="server" Width="110px" Text='<%# Eval("RefPRPO") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SALES INVOICE" ItemStyle-CssClass="columnSpace">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtSalesInvoice" runat="server" Width="110px" Text='<%# Eval("SalesInvoice") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BRAND / MACHINE" ItemStyle-CssClass="columnSpace">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtBrandMachine" runat="server" Width="110px" Text='<%# Eval("BrandMachineName") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ITEM NAME" ItemStyle-CssClass="columnSpace">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtItemName" runat="server" Width="178px" Text='<%# Eval("ItemName") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SPECIFICATION" ItemStyle-CssClass="columnSpace">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtSpecification" runat="server" Width="200px" Text='<%# Eval("Specification") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QTY" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtQuantity" runat="server" Width="60px" Text='<%# Eval("TotalQuantity") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtUnitOfMeasure" runat="server" Width="60px" Text='<%# Eval("UOM_Description") %>' />
                                                </ItemTemplate>                    
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SERIAL NO" ItemStyle-CssClass="columnSpace">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtSerialNo" runat="server" Width="100px" Text='<%# Eval("SerialNo") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="DEL. QTY." ItemStyle-CssClass="columnSpace" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtActualQty" runat="server" Width="60px" Text='<%# Eval("Warehouse_TotalActualQuantity") %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="REM. QTY." ItemStyle-CssClass="columnSpace" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtRemainingQty" runat="server" Width="60px" Text='<%# Eval("Warehouse_RemainingQuantity") %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="AITD" ItemStyle-CssClass="columnSpace" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbAITD" runat="server" Text="*" Width="40px" />
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                        </Columns>

                                    </asp:GridView>    
                                    </div>                                                                                 
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="row clearfix"  id="divAttachment" runat="server" visible="false">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1200px;">
                            <div class="card">
                                <div class="body" style="margin-top:-23px; min-height:20px; width:1060px;">
                                    <table style="width:100%; margin-top:10px; color:Gray;">
                                      <tr>                                         
                                        <th>ATTACHMENT</th>                          
                                      </tr>
                                      <tr>                                                                                                        
                                        <td><asp:FileUpload ID="fu" runat="server" Width="190px" EnableViewState="true" accept=".pdf" /></td>   
                                      </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                     </div>
                                         
                    <div class="row clearfix" id="divActualDelivery" runat="server" visible="false">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1200px;">
                            <div class="card">
                                <div class="body" style="margin-top:-23px; min-height:100px; width:1150px;">
                                
                                    <table style="width:100%; margin-top:10px; color:Gray; text-align:left;">
                                        <tr>
                                            <p style="font-size:14px; font-weight:bold;">ACTUAL DELIVERY</p>
                                        </tr>
                                        <tr>                                                                    
                                            <p id="pError" runat="server" style="font-size:12px; font-weight:bold; color:Red"></p>
                                        </tr>
                                        <tr>
                                            <th>ITEM NAME</th>
                                            <th>ACTUAL QUANTITY</th>
                                            <th>WITH DOCUMENTS?</th>
                                            <th>ACTION</th>
                                        </tr>
                                        <tr>
                                            <td><asp:DropDownList ID="ddActualItemName" runat="server" Width="700px" Height="22px" onChange="getSelectedItemName(this)" /></td>
                                            <td><asp:TextBox ID="txtActualQuantity2" runat="server" Width="150px" Height="22px" onkeypress="return isNumberKey(event)" /></td>
                                            <td>
                                                <asp:DropDownList ID="ddWithDocuments" runat="server" Width="100px" Height="22px" onChange="getSelectedWithDocuments(this)">
                                                    <asp:ListItem Text="" Value="" />
                                                    <asp:ListItem Text="YES" Value="YES" />
                                                    <asp:ListItem Text="NO" Value="NO" />
                                                </asp:DropDownList>
                                            </td>
                                            <td><asp:LinkButton ID="lbAddNewActual" runat="server" Text="ADD NEW" OnClientClick="return AddRow()" /></td>
                                        </tr>
                                        <tr>
                                            <th>&nbsp;</th>
                                        </tr>
                                        <tr>
                                            <asp:GridView ID="gvActualDelivery" runat="server" 
                                                          AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px" 
                                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" 
                                                          FooterStyle-Font-Size="10px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="REFID" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# Eval("Warehouse_DetailsRefId")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ITEM NAME" ItemStyle-Width="550px" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# Eval("Warehouse_ItemName")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ACTUAL QUANTITY" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# Eval("Warehouse_TotalActualQuantity") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TRANSACTION DATE" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# Eval("Warehouse_DeliveredDate")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NEW ENTRY" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# Eval("Warehouse_NewEntry")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WITH DOCUMENTS?" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%# Eval("Warehouse_WithDocuments")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </tr>
                                    </table>
                                    
                                    
                                
                                </div>
                            </div>
                        </div>
                    </div>
                    
                   <div id="divNote" runat="server" style="margin-top:0px; padding-bottom:10px; min-height:20px; width:1200px; text-align:center;" visible="false">
                        <p style="color:Red; font-size:16px; font-weight:bold;">NOTE: Make sure that you check the AITD (ACTUAL ITEMS TO DELIVER) before clicking SET THE DELIVERY IN-PROGRESS!</p>
                   </div>
                   
                   <div style="margin-top:0px; min-height:20px; width:1200px;">
                        <asp:Button ID="btnInProgress" runat="server" Text="SET THE DELIVERY IN-PROGRESS" Visible="false" CssClass="btn bg-light-blue waves-effect" OnClick="btnInProgress_Click" />
                        <asp:Button ID="btnConfirmDelivery" runat="server" Text="CONFIRM DELIVERY" Visible="false" OnClientClick="ManipulateGrid()" CssClass="btn bg-light-green waves-effect" OnClick="btnConfirmDelivery_Click" />
                        <asp:Button ID="btnClose" runat="server" Width="100px" Text="CLOSE" CssClass="btn btn-block bg-pink waves-effect" OnClientClick="hideDialog();" />
                    </div>
                    
                    
                                      
                </div>
            </div>
            
            
             <div class="hover_bkgr_fricc2" style="overflow:auto;">
                
                <div>
                    <table style="width:100%; margin-top:10px; color:Gray; text-align:left;">
                        <tr>
                            <td style="font-size:18px;"><asp:Label ID="lblCTRLNoWithoutDocument" runat="server" Font-Bold="true" /></td>
                        </tr>
                        
                        <tr>
                        
                            <asp:GridView ID="gvWithoutDocuments" runat="server" 
                                          AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px" 
                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" 
                                          FooterStyle-Font-Size="10px">
                                <Columns>
                                    <asp:TemplateField HeaderText="REFID" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefId" runat="server" Visible="false" Text='<%# Eval("RefId") %>' ></asp:Label>
                                            <asp:Label ID="lblDetailsRefId" runat="server" Width="80px" Text='<%# Eval("Warehouse_DetailsRefId") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ITEM NAME" ItemStyle-Width="550px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Width="80px" Text='<%# Eval("Warehouse_ItemName") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ACTUAL QUANTITY" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActualQuantity" runat="server" Width="80px" Text='<%# Eval("Warehouse_TotalActualQuantity") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TRANSACTION DATE" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionDate" runat="server" Width="80px" Text='<%# Eval("Warehouse_DeliveredDate") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                           
                                    <asp:TemplateField HeaderText="DOCUMENTS" ItemStyle-Width="220px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:FileUpload ID="fuWithoutDocuments" runat="server" Width="220px" EnableViewState="true" accept=".pdf" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        
                        </tr>
                        
                    
                    </table>
                    
                    <div style="margin-top:0px; min-height:20px; width:1200px;">
                        <asp:Button ID="btnConfirmDocuments" runat="server" Text="CONFIRM DOCUMENTS" CssClass="btn bg-light-green waves-effect" OnClick="btnConfirmDocuments_Click" />
                        <asp:Button ID="btnClose2" runat="server" Width="100px" Text="CLOSE" CssClass="btn btn-block bg-pink waves-effect" OnClientClick="hideDialog2();" />
                    </div>
                
                </div>
                
                
                
             </div>
             
             
             
             <div class="hover_bkgr_fricc3" style="overflow:auto;">
                
                <div>
                    <table style="width:100%; margin-top:10px; color:Gray; text-align:left;">
                        <tr>
                            <td style="font-size:18px;">DOWNLOAD REPORT</td>
                        </tr>
                        
                        <tr>
                            <th>FROM (IMPEX APPROVED DATE)</th>
                            <th>TO (IMPEX APPROVED DATE)</th>
                            <th>REPORT TYPE</th>
                            <th>STATUS</th>
                            <th>PEZA / NON-PEZA</th>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="txtFrom" runat="server" Width="205px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                            <td><asp:TextBox ID="txtTo" runat="server" Width="205px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>   
                            <td>
                                <asp:DropDownList ID="ddReportType" runat="server" Width="200px" Height="28px" class="form-control">
                                    <asp:ListItem Text="ALL ITEMS" Value="ALL ITEMS" />
                                    <asp:ListItem Text="LIQUIDATION LEDGER" Value="LIQUIDATION LEDGER" />                                    
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddReportStatus" runat="server" Width="200px" Height="28px" class="form-control">
                                    <asp:ListItem Text="ALL" Value="ALL" />
                                    <asp:ListItem Text="APPROVED / WAITING FOR DELIVERY" Value="APPROVED / WAITING FOR DELIVERY" />
                                    <asp:ListItem Text="DELIVERY IN-PROGRESS" Value="DELIVERY IN-PROGRESS" />
                                    <asp:ListItem Text="DELIVERED WITH PENDING ITEMS" Value="DELIVERED WITH PENDING ITEMS" />
                                    <asp:ListItem Text="DELIVERED" Value="DELIVERED" />
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddPezaNonPezaReport" runat="server" Width="100px" Height="28px" class="form-control">
                                    <asp:ListItem Text="ALL" Value="ALL" />
                                    <asp:ListItem Text="PEZA" Value="PEZA" />
                                    <asp:ListItem Text="NON PEZA" Value="NON PEZA" />
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnDownloadReport2" runat="server" Text="DOWNLOAD REPORT" CssClass="btn bg-light-green waves-effect" OnClick="btnDownloadReport2_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnCloseReport" runat="server" Width="100px" Text="CLOSE" CssClass="btn btn-block bg-pink waves-effect" OnClientClick="hideDialog3();" />
                            </td>
                        </tr>
                    
                    </table>
                
                </div>
                
                
                
             </div>
             
             
            
            <asp:HiddenField ID="buyerCategory" runat="server" />    
            <asp:HiddenField ID="selectedItemName" runat="server" />
            <asp:HiddenField ID="selectedWithDocuments" runat="server" />  
            <asp:HiddenField ID="csvActualCollections" runat="server" />  
            <asp:HiddenField ID="csvActualCollections2" runat="server" />  
            <asp:HiddenField ID="hiddenUserId" runat="server" />   
            <asp:HiddenField ID="checkIfSelected" runat="server" />             
                                                                       
            
    </div>
            
    </section>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID = "btnConfirmDelivery" />
        <asp:PostBackTrigger ControlID = "btnSubmit" />  
        <asp:PostBackTrigger ControlID = "btnInProgress" />   
        <asp:PostBackTrigger ControlID = "btnConfirmDocuments" />  
    </Triggers>
    
    </asp:UpdatePanel>
    
</asp:Content>
