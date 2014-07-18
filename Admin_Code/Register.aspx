<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Register" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<% if (PopupMsg!=null)
   { %>
<script type="text/JavaScript">

    function ShowMessage() {
        alert("<%=PopupMsg %>");
    }
    window.onload = ShowMessage; 

</script>
<% } %>
<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../Styles/pure-min.css"/>
</head>
<body>
    <form id="form1" runat="server" class="pure-form pure-form-aligned" style="margin-left:10em;margin-top:2em;">
    
	<div id="loginCredentialsDiv" style="display:block;" runat="server">
         <div class="pure-control-group">
         <p id="loginWarningUp" runat="server" style="color:red;"></p>
         </div>
        <div class="pure-control-group">
		    <label for="name">Name: </label>
		    <input runat="server" type="text" id="name" value="" name="name" width="200px"/>
	    </div>

        <div class="pure-control-group">
		    <label for="name">UserName: </label>
		    <input runat="server" type="text" id="userName" value="" name="userName" width="200px"/>
	    </div>

	    <div class="pure-control-group">
		    <label for="password">Password: </label>
		    <input runat="server" type="password" id="password" name="password"/>
	    </div>
        <div class="pure-control-group">
		    <label for="password_again">Confirm Password: </label>
		    <input runat="server" type="password" id="password_again" name="password_again"/>
	    </div>
	    <div class="pure-control-group">
		    <label for="type">Type: </label>
		    <select name="type" id="type" runat="server"> 
			    <option value="1" runat="server">Permanent Staff(Faculty)</option>
			    <option value="2" runat="server">Permanent Staff(Other)</option>
			    <option value="3" runat="server">Non-Permanent</option>
		    </select>
	    </div>

	    <div class="pure-control-group">
		    <label for="actorrank">Permissions: </label>
		    <select name="actorrank" id="actorrank" runat="server"> 
			    <option value="0" selected="selected">User</option>
											
		    </select>
	    </div>
        <div class="pure-control" style="margin-top:60px;">
		    <input runat="server" type="submit" id="loginNext"  onserverclick="LoginNext_Click" style="margin-left:50%;" class="pure-button pure-button-primary" value="Next >>" width="100px"/>
	    </div>
    </div>

    <div id="profileInfoDiv" style="display:none;" runat="server">
        <h3>PROFILE INFO</h3>
        <div class="pure-control-group">
         <p id="ProfileWarningUp" runat="server" style="color:red;"></p>
         </div>
	    <div class="pure-control-group">
		    <label>Designation</label>
		    <input runat="server" type="text" id="designation" name="designation" value=""/> 			
	    </div>
	    <div class="pure-control-group">
		    <label>Department</label>
		    <input runat="server" type="text" id="department" name="department" value=""/> 
	    </div>
    
	    <div class="pure-control-group">
		    <label for="username">Employee No.: </label>
		    <input runat="server" type="text" name="employeeNo" id="employeeNo" value="" autocomplete="off"/>
	    </div>

	    <div class="pure-control-group">
		    <label>Age</label>
		    <input runat="server" type="text" id="Age" name="Age" value=""/>  
	    </div>
         <div class="pure-control-group">
		    <label for="Sex">Sex: </label>
		    <select name="Sex" id="Sex" runat="server"> 
			    <option value="0" selected="selected">Male</option>
                <option value="1">Female</option>
		    </select>
	    </div>

        <div style="margin-top:60px;">
            <div class="pure-control">
		        <input runat="server" type="submit" id="profileBack"  style="float:left; margin-left:60px;" onserverclick="ProfileBack_Click" class="pure-button pure-button-primary" value="<< Back" width="100px"/>
	        </div>
            <div class="pure-control">
		        <input runat="server" type="submit" id="ProfileNext"  style="margin-left:90px;" onserverclick="ProfileNext_Click" class="pure-button pure-button-primary" value="Next >>" width="100px"/>
	        </div>
        </div>
    </div>

    <div id="biodataDiv" style="display:none;" runat="server">
        <h3>BIODATA</h3>
        <div class="pure-control-group">
         <p id="biodataWarningUp" runat="server" style="color:red;"></p>
         </div>
	    <div class="pure-control-group">
		    <label>Father's Name</label>
		    <input runat="server" type="text" id="fathersname" name="fathersname" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>Spouse Name</label>
		    <input runat="server" type="text" id="spousename" name="spousename" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>Nationality (or Eligibity Certificate Date)</label>
		    <input runat="server" type="text" id="nationality" name="nationality" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>Religion</label>
		    <input runat="server" type="text" id="religion" name="religion" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>SC/ST(NA if neither)</label>
		    <input runat="server" type="text" id="scst" name="scst" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>Name of Caste</label>
		    <input runat="server" type="text" id="casteName" name="casteName" value=""/>
	    </div>
	    <div class="pure-control-group">			
		    <label>DOB</label>
            <%--<ajaxToolKit:ToolkitScriptManager ID="ToolkitScriptManagerDOB" runat="server"></ajaxToolKit:ToolkitScriptManager>--%>
		    <asp:TextBox ID="dob" name="dob" runat="server"></asp:TextBox>
            <%--<ajaxToolKit:CalenderExtender ID="CalenderExtenderDOB" TargetControlID="dob" runat="server"></ajaxToolKit:CalenderExtender>--%>
	    </div>
	    <label><strong>Educational Qualifications</strong></label>
	    <div class="pure-control-group">
		    <label>When appointed</label>
		    <input runat="server" type="text" id="appointed" name="appointed" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>Subsequently acquired</label>
		    <input runat="server" type="text" id="subsequent" name="subsequent" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>Height(also mention the units)</label>
		    <input runat="server" type="text" id="height" name="height" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>Identification Marks(Seperate with commas)</label>
		    <textarea runat="server" cols="60" rows="10" id="identification" name="identification" text=""></textarea>	
	    </div>
	    <div class="pure-control-group">
		    <label>Permanent Home Address</label>
		    <textarea runat="server" cols="60" rows="10" id="address" name="address" text=""></textarea>
	    </div>
        <div style="margin-top:60px;">
            <div class="pure-control">
		        <input runat="server" type="submit" id="biodataBack" style="float:left; margin-left:60px;" onserverclick="BiodataBack_Click" class="pure-button pure-button-primary" value="<< Back" width="100px"/>
	        </div>
            <div class="pure-control">
		        <input runat="server" type="submit" id="biodataNext" style="margin-left:90px;" onserverclick="BiodataNext_Click" class="pure-button pure-button-primary" value="Next >>" width="100px"/>
	        </div>
        </div>
    </div>

    <div id="certificateAttestationDiv" style="display:none;" runat="server">
        <h3>CERTIFICATION AND ATTESTATION</h3>
        <div class="pure-control-group">
         <p id="certificationWarningUp" runat="server" style="color:red;"></p>
         </div>
	    <label><strong>Medical Examination</strong></label>
	    <div class="pure-control-group">
		    <label>Conducted By</label>
		    <input runat="server" type="text" id="medicalTestBy" name="medicalTestBy" value=""/>  
	    </div>
	    <div class="pure-control-group">
		    <label>Date</label>
		    <asp:Label runat="server" TextMode="Date" id="medicalTestDate" name="medicalTestDate" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>Serial No.</label>
		    <input runat="server" type="text" id="medicalTestSrNo" name="medicalTestSrNo" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>Character and Antecedents(Sr. No)</label>
		    <input runat="server" type="text" id="characterSrNo" name="characterSrNo" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>Verification of Biodata(Sr. No.)</label>
		    <input runat="server" type="text" id="biodataVerificationSrNo" name="biodataVerificationSrNo" value=""/>
	    </div><br/>
	    <label>Requirement Benefit Scheme</label>
	    <div class="pure-control-group">			
		    <label>Scheme Elected</label>
		    <input runat="server" type="text" id="schemeElected" name="schemeElected" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>File No.</label>
		    <input runat="server" type="text" id="schemeFileNo" name="schemeFileNo" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>Date</label>
		    <asp:Label runat="server" TextMode="Date" id="schemeDate" name="schemeDate" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>PRAN No.</label>
		    <input runat="server" type="text" id="pranNo" name="pranNo" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>Nomination for NPS(Sr. No.)</label>
		    <input runat="server" type="text" id="npsFileNo" name="npsFileNo" value=""/>
	    </div>
        <div style="margin-top:60px;">
            <div class="pure-control">
		        <input runat="server" type="submit" id="certificationBack" style="float:left; margin-left:60px;" onserverclick="CertificationBack_Click" class="pure-button pure-button-primary" value="<< Back" width="100px"/>
	        </div>
            <div class="pure-control">
		        <input runat="server" type="submit" id="certificationNext" style="margin-left:90px;" onserverclick="CertificationNext_Click" class="pure-button pure-button-primary" value="Next >>" width="100px"/>
	        </div>
        </div>
    </div>

    <div id="ltcDeclarationDiv" style="display:none;" runat="server">
        <h3> DECLARATION OF HOME TOWN FOR LTC</h3>
        <div class="pure-control-group">
         <p id="declarationWarningUp" runat="server" style="color:red;"></p>
         </div>
	    <div class="pure-control-group">
		    <label>Hometown</label>
		    <input runat="server" type="text" id="homeTown" name="homeTown" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>Taluka</label>
		    <input runat="server" type="text" id="taluka" name="taluka" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>District</label>
		    <input runat="server" type="text" id="district" name="district" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>State</label>
		    <input runat="server" type="text" id="state" name="state" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>Nearest Railway Station</label>
		    <input runat="server" type="text" id="nearestRlyStation" name="nearestRlyStation" value=""/>
	    </div>
	    <em>Reasons for declaration</em>
	    <div class="pure-control-group">
		    <label>Reason 1 :</label>
		    <input runat="server" type="text" id="reasonOneForDeclaration" name="reasonOneForDeclaration" value=""/>
	    </div>
	    <div class="pure-control-group">
		    <label>Reason 2 :</label>
		    <input runat="server" type="text" id="reasonTwoForDeclaration" name="reasonTwoForDeclaration" value=""/>
	    </div>

	    <input id="Hidden1" runat="server" type="hidden" name="token" value="0c2f422816252d7fa6ad0ea203f8509d"/>	
         <div style="margin-top:60px;">
            <div class="pure-control">
		        <input runat="server" type="submit" style="margin-left:60px;float:left;"  id="ltcDeclarationBack" onserverclick="LtcDeclarationBack_Click" class="pure-button pure-button-primary" value="<< Back" width="100px"/>
	        </div>		
	        <div class="pure-control">
		        <input runat="server" type="submit" style="margin-left:90px;" id="submit" onserverclick="Submit_Click" class="pure-button pure-button-primary" value="Register" width="100px"/>
	        </div>
         </div>
       </div>
       
    </form>
</body>
</html>
