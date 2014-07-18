<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication2.Login" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    
    <title>OAP.v.02 &ndash; Home</title>

<link rel="stylesheet" href="Styles/pure-min.css" type="text/css" charset="utf-8"/>
  
    <link rel="stylesheet" href="Styles/grids-responsive-min.css" type="text/css" charset="utf-8"/>
  
        <link rel="stylesheet" href="/Styles/marketing.css" type="text/css" charset="utf-8"/>

<link rel="stylesheet" href="Styles/font-awesome.css" type="text/css" charset="utf-8"/>
</head>
<body>
    <form id="form1" runat="server">

<div class="header">
    <div class="home-menu pure-menu pure-menu-open pure-menu-horizontal pure-menu-fixed">
        <a class="pure-menu-heading" href="" style="float:left;clear:right;display:inline-block;">OAP</a>

        <ul style="float:right;display:inline-block;">
            <li class="pure-menu-selected"><a href="#">Home</a></li>
            <li><a href="#">Tour</a></li>
            <li><a href="#">Sign Up</a></li>
        </ul>
    </div>
</div>
<div class="content-wrapper">
    
    <div class="content">
        <h2 class="content-head is-center">Indian Institute of Technology Indore</h2>

        <div class="pure-g">
            <div class="l-box-lrg pure-u-md-3-5">
                <h4>About IITI</h4>
                <p>

<%--IIT Indore started functioning in 2009. It was setup under the mentorship of IIT Bombay.

B.Tech Courses offered:

Computer Science and Engineering
Electrical Engineering
Mechanical Engineering

     Each of these is a 4 year course and has a maximum strength of 40 students.


Information about faculty and their research work can be found at this link.

Information regarding course curriculum along with academic policies can be found at this link.

Campuses

Silver Springs Township (Hostel)

One of the best townships in Indore, IITI students are currently accommodated in row houses, terrace cottages and flats at Silver Springs. The rooms are spacious and the area is peaceful with ample number of lawns.

PACL Campus

Academic campus that houses most of the classrooms and engineering labs. The engineering departments are based here.

IET Campus

Academic campus that houses the sciences and humanities departments and their respective labs.

Simrol Permanent Campus

IIT Indore's permanent campus is spreads over an area of about 510 acres at Simrol, a location about 25 km from the city of Indore.


Accommodation is provided at Silver Springs hostel. A few first year classes and labs take place at the IET campus. The rest and most are held at PACL. IIT Indore has a fleet of 15 buses that ply between these campuses in accordance with class timings. There is also a bus plying to the city for shopping and recreational purposes every Saturday and Sunday.

Travel times:

Silver Springs to PACL: 25 mins
Silver Springs to IET: 20 mins
Silver Springs to city/railway station: 40 mins
IET to city: 5 mins

For address details, have a look at this link.

Weather

Indore has pleasant weather for most months of the year.

It is coldest during mid-Jan when the temperature drops till 10 degrees Celsius which the average temperature is 14-15 degrees. The month of April gets hot as temperatures touch 35-38 degrees at times. May-July is vacation so the students aren’t affected by heat too much.

There is heavy rainfall in the months of July and August.

Year round temperatures can be found at this link.

Facilities

Silver Springs hostel has a football ground with floodlights, basketball court, tennis courts and volleyball net. IITI maintains a table tennis table, gym and student activity centre here.

The mess serves breakfast, lunch, high tea with snacks and dinner. It operates a night canteen during exam time.

Dispensary always has a doctor to attend and is open every day. IITI also has a 24X7 ambulance available at hostel for emergency medical situations. It also makes trips to the hospital every day. Students are covered by medical insurance and all treatment is free, even at the hospital.

The student activity centre is where students and student clubs have meetings, events and activities.

La Fresco is IIT Indore’s own general store and has been started and is run by students themselves. All day-to-day requirements like cosmetics, packed food, stationery, cold-drinks and refreshments are available here. It even has a photo-copying machine and printing facility.--%>

                </p>

                <h4>Placements</h4>
                <p>
                    
IIT Indore had its first placement session two years ago in the academic year 2012-2013 that went in accordance of brand IIT. Companies like Microsoft, Google, Honda Cars, Qualcomm and many other leaders in the industry came forward to recruit from our first batch. The one most common response that the Placement Office received from the companies was - "How did you manage all this in the first year itself?"

After the second session of placements that concluded this year, we've got renewed belief in the standing that our college holds in the corporate world, with our recruitment percentages acing the 85% mark and even rising to 100% in CSE this year!

We, as a placement team believe in keeping our head down and churning out results, which may have the downside of our college not being marketed enough in the country, yet it ensures that our students are given a chance to get interviewed by the finest professionals, with some of the most competitive packages that are offered. Yet, to satisfy the thirst for facts, we've had students selected in Epic Systems in USA, we've had Facebook conducting interviews last year and we've seen the other end of the spectrum where our students are heading to CERN (Switzerland), Germany and even Croatia for research internships, along with Microsoft, CISCO, CMC and many others being leading names in our corporate intern recruitment program as well.


Awards and Achievements

Dr. Rajneesh Misra awarded INSA Young Scientist Medal
IIT Indore team wins Iris liveness detection competition
Mr. Pooran Singh awarded Fulbright-Nehru Research Fellowship
Dr. M. Anbarasu selected for DAE Young Scientist Research Award
IIT students selected for Diplome d'Ingenieur (M.Tech.) programme
IIT Indore’s team Paradigm Shift makes it to the ACM ICPC World Finals twice in two years.
IIT-Indore students develop technology for laptop and cellphones
Won the 1st Mobile Iris Liveness Detection Competition.
IIT Indore joins the ALICE International Collaboration at the Large Hadron Collider (LHC), CERN, Geneva, Switzerland.

                </p>
            </div>
            
            <div class="l-box-lrg pure-u-md-2-5" style="float:right;position:fixed;">
                <div class="pure-form">
                        <fieldset>
                        <asp:Label ID="LWarning" runat="server" ForeColor="Red"></asp:Label><br/>

                        <asp:Label ID="UserNameLabel" style="float:left;width:30%;" runat="server">Username:</asp:Label>
                        <asp:TextBox ID="UserName" style="float:left;width:70%;" runat="server" CssClass="textEntry"></asp:TextBox>
                        <br />
                        <asp:Label ID="PasswordLabel" style="float:left;width:30%;" runat="server">Password:</asp:Label>
                        <asp:TextBox ID="Password" style="float:left;width:70%;" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <br />
                        <asp:CheckBox ID="RememberMeChkBx" style="float:left;clear:left;width:10%;" runat="server"/>
                        <asp:Label ID="RememberMeLabel" style="float:left;width:90%;" runat="server" CssClass="inline">Keep me logged in</asp:Label>
                        </fieldset>
                    
                    <asp:Button ID="LoginButton" runat="server" CssClass="pure-button-primary" style="padding:10px;" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup"/>
                
                </div>
            </div>
        </div>

    </div>

    <div class="footer l-box is-center">
        Footer
    </div>

</div>
    </form>
</body>
</html>
