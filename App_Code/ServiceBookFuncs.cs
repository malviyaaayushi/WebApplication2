using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


public class ServiceBookFuncs
{
    public Biodata biodata;
    public LtcDeclaration ltcDeclaration;
    public List<PreviousQualifyingService> previousQualifyingServiceList;
    public List<ForeignService> foreignServiceList;
    public LtcDependents ltcDependents;
    public List<LtcRecord> ltcRecordList;
    public CertificateAttestation certificateAttestation;
    public List<LtcDependents> ltcDependentsList;
    public List<KeyValuePair<string, int>> balance;
    public List<ServiceRegister> serviceRegisterList;
    public string query;

    public void fetchData()
    {
        Database.connectToDatabse();
        
        try
        {
            fetchBiodata();
            fetchPreviousQualifyingService();
            fetchCertificateAttestation();
            fetchForeignServices();
            fetchLtcDependents();
            fetchLTCRecord();
            fetchServiceRegister();
            fetchLtcDeclaration();
        }
        catch (Exception ex)
        {
            AUserSession.Current.Warning = ex.Message;
        }
        Database.disconnectToDatabase();
    }

    public void fetchForeignServices()
    {
        foreignServiceList = new List<ForeignService>();
        try
        {
            query = "SELECT * from ForeignService where userId='" + AUserSession.Current.ThisUser.UserId + "'";
            Database.executeQuery(query);
            DataTable dtable = Database.getDataTable();

            foreach (DataRow row in dtable.Rows)
            {
                ForeignService foreign = new ForeignService();
                foreign.From = row.Field<DateTime>("fromPeriod");
                foreign.To = row.Field<DateTime>("toPeriod");
                foreign.FEmployerDetails = row.Field<string>("foreignEmployerDetails");
                foreign.FLPCPFpayable = row.Field<string>("LPCPFpayable");
                foreign.FLPCPFreceived = row.Field<string>("LPCPFreceived");
                foreign.FMRNo = row.Field<string>("MRNo");
                foreign.FMRDate = row.Field<DateTime>("MRDate");
                foreignServiceList.Add(foreign);

            }

        }
        catch (Exception ex)
        {
            AUserSession.Current.Warning = ex.Message;
        }
    }

    public void fetchBiodata()
    {
        query = "SELECT * from Biodata where userId='" + AUserSession.Current.ThisUser.UserId + "'";
        Database.executeQuery(query);

        biodata = new Biodata();

        if (Database.getDataTable().Rows.Count == 1)
        {
            DataRow row = Database.getDataTable().Rows[0];
            
            biodata.Name = row.Field<string>("name");
            biodata.FatherName = row.Field<string>("fatherName");
            biodata.SpouseName = row.Field<string>("spouseName");
            biodata.Nationality = row.Field<string>("nationality");
            biodata.Religion = row.Field<string>("religion");
            biodata.ScheduledCaste = row.Field<string>("scheduledCaste");
            biodata.CasteName = row.Field<string>("casteName");
            biodata.Dob = row.Field<DateTime>("DOB");
            biodata.QualificationWhenAppointed = row.Field<string>("qualificationWhenAppointed");
            biodata.QualificationAfterwards = row.Field<string>("qualificationAfterwards");
            biodata.HeightCm = row.Field<string>("heightCm");
            biodata.IdentificationMarks = row.Field<string>("identificationMarks");
            biodata.PermanentHomeAddress = row.Field<string>("permanentHomeAddress");
        }
        else
        {
            AUserSession.Current.Warning = "Invalid User";
        }
    }

    public void fetchPreviousQualifyingService()
    {
        try
        {
            query = "SELECT * from PreviousQualifyingService where userId='" + AUserSession.Current.ThisUser.UserId + "'";
            Database.executeQuery(query);

            previousQualifyingServiceList = new List<PreviousQualifyingService>();
            PreviousQualifyingService previousQualifyingService;
            DataTable dtable = Database.getDataTable();

            foreach (DataRow row in dtable.Rows)
            {
                previousQualifyingService = new PreviousQualifyingService();
                previousQualifyingService.From = row.Field<DateTime>("from");
                previousQualifyingService.To = row.Field<DateTime>("to");
                previousQualifyingService.PostHeld = row.Field<string>("postHeld");
                previousQualifyingService.Purpose = row.Field<string>("purpose");

                previousQualifyingServiceList.Add(previousQualifyingService);

            }

        }
        catch (Exception ex)
        {
            AUserSession.Current.Warning = ex.Message;
        }
    }

    public void fetchLeaveBalance()
    {
        Database.connectToDatabse();
        query = "SELECT * from LeaveBalance where userId='" + AUserSession.Current.ThisUser.UserId + "'";
        Database.executeQuery(query);
        balance = new List<KeyValuePair<string, int>>();
        if (Database.getDataTable().Rows.Count == 1)
        {
            DataRow row = Database.getDataTable().Rows[0];
            
            balance.Add(new KeyValuePair<string, int>("casualLeaveBalance", row.Field<int>("casualLeaveBalance")));
            balance.Add(new KeyValuePair<string, int>("clBalance", row.Field<int>("clBalance")));
            balance.Add(new KeyValuePair<string, int>("specialClBalance", row.Field<int>("specialClBalance")));
            balance.Add(new KeyValuePair<string, int>("specialLeaveBalance", row.Field<int>("specialLeaveBalance")));
            balance.Add(new KeyValuePair<string, int>("halfPayLeaveBalance", row.Field<int>("halfPayLeaveBalance")));
            balance.Add(new KeyValuePair<string, int>("commutedLeaveBalance", row.Field<int>("commutedLeaveBalance")));
            balance.Add(new KeyValuePair<string, int>("earnedLeaveBalance", row.Field<int>("earnedLeaveBalance")));
            balance.Add(new KeyValuePair<string, int>("extraOrdinaryLeaveBalance", row.Field<int>("extraOrdinaryLeaveBalance")));
            balance.Add(new KeyValuePair<string, int>("maternityLeaveBalance", row.Field<int>("maternityLeaveBalance")));
            balance.Add(new KeyValuePair<string, int>("hospitalLeaveBalance", row.Field<int>("hospitalLeaveBalance")));
            balance.Add(new KeyValuePair<string, int>("quarantineLeaveBalance", row.Field<int>("quarantineLeaveBalance")));
            balance.Add(new KeyValuePair<string, int>("leaveNotLeaveBalance", row.Field<int>("leaveNotLeaveBalance")));
            balance.Add(new KeyValuePair<string, int>("sabbaticalLeaveBalance", row.Field<int>("sabbaticalLeaveBalance")));
            balance.Add(new KeyValuePair<string, int>("vacationBalance", row.Field<int>("vacationBalance")));
        }
        else
        {
            AUserSession.Current.Warning = "Invalid User";
        }
        Database.disconnectToDatabase();
    }

    public void fetchCertificateAttestation()
    {
        query = "SELECT * from CertificationAttestation where userId='" + AUserSession.Current.ThisUser.UserId + "'";
        Database.executeQuery(query);
        certificateAttestation = new CertificateAttestation();
        if (Database.getDataTable().Rows.Count == 1)
        {
            DataRow row = Database.getDataTable().Rows[0];            
            certificateAttestation.MedicalTestBy = row.Field<string>("MedicalTestBy");
            certificateAttestation.MedicalTestDate = row.Field<DateTime>("MedicalTestDate");
            certificateAttestation.MedicalFileNo = row.Field<string>("medicalFileNo");
            certificateAttestation.AntecedentsFileNo = row.Field<string>("antecedentsFileNo");
            certificateAttestation.BiodataFileNo = row.Field<string>("biodataFileNo");
            certificateAttestation.RbScheme = row.Field<string>("RBScheme");
            certificateAttestation.RbDate = row.Field<DateTime>("RBDate");
            certificateAttestation.RbFileNo = row.Field<string>("RBFileNo");
            certificateAttestation.PranNo = row.Field<string>("pranNo");
            certificateAttestation.NpsFileNo = row.Field<string>("npsFileNo");
        }
        else
        {
            AUserSession.Current.Warning = "Invalid User";
        }
    }

    public void fetchLTCRecord()
    {
        query = "SELECT * from LeaveTravelConcession where userId='" + AUserSession.Current.ThisUser.UserId + "'";
        Database.executeQuery(query);

        ltcRecordList = new List<LtcRecord>();

        if (Database.getDataTable().Rows.Count > 0)
        {
            int size = Database.getDataTable().Rows.Count;
            DataRow row = null;

            for (int i = 0; i < size; i++)
            {
                row = Database.getDataTable().Rows[i];

                LtcRecord record = new LtcRecord();
                record.BlockYear = row.Field<int>("blockYear");
                record.FromDate = row.Field<DateTime>("fromDate");
                record.ToDate = row.Field<DateTime>("toDate");
                record.RelativeName = row.Field<string>("relativeName");
                record.Relation = row.Field<string>("relation");
                record.HomeTown = row.Field<string>("homeTown");
                record.OtherPlaces = row.Field<string>("otherPlaces");
                record.AmountPaid = row.Field<string>("amountPaid");
                record.CertifyingOfficer = row.Field<string>("certifyingOfficer");
                //ltcRecord.Insert(i, record);
                ltcRecordList.Add(record);
            }

        }
        else
        {
            AUserSession.Current.Warning = "Invalid User";
        }
    }
    
    public void fetchServiceRegister()
    {
        query = "SELECT * from ServiceRegister where userId='" + AUserSession.Current.ThisUser.UserId + "'";
        Database.executeQuery(query);

        serviceRegisterList = new List<ServiceRegister>(); ;

        if (Database.getDataTable().Rows.Count > 0)
        {
            foreach (DataRow row in Database.getDataTable().Rows)
            {
                ServiceRegister record = new ServiceRegister();

                record.PostAndPayDescription = row.Field<string>("postAndPayDescription").ToString();
                record.PermanentOrTemporary = row.Field<string>("permanentOrTemporary").ToString();
                record.Incumbent = row.Field<string>("incumbent").ToString();
                record.PostHeldPermanently = row.Field<string>("postHeldPermanently").ToString();
                record.PayInPermanentPost = row.Field<string>("payInPermanentPost");
                record.OfficiatingPay = row.Field<string>("officiatingPay");
                record.OtherPay = row.Field<string>("otherPay");
                record.FromPeriod = row.Field<DateTime>("fromPeriod");
                record.ToPeriod = row.Field<DateTime>("toPeriod");
                record.Events1to8 = row.Field<string>("events1to8").ToString();
                record.LeaveDescription = row.Field<string>("leaveDescription").ToString();
                record.PunishmentReference = row.Field<string>("punishmentReference").ToString();
                record.Remarks = row.Field<string>("remarks").ToString();

                serviceRegisterList.Add(record);
            }

        }
        else
        {
            AUserSession.Current.Warning = "Invalid User";
        }
    }

    public void fetchLtcDeclaration()
    {
        query = "SELECT * from LtcDeclaration where userId='" + AUserSession.Current.ThisUser.UserId + "'";
        Database.executeQuery(query);

        ltcDeclaration = new LtcDeclaration();

        if (Database.getDataTable().Rows.Count == 1)
        {
            DataRow row = Database.getDataTable().Rows[0];

            ltcDeclaration.HomeTown = row.Field<string>("homeTown");
            ltcDeclaration.Taluka = row.Field<string>("taluka");
            ltcDeclaration.District = row.Field<string>("district");
            ltcDeclaration.State = row.Field<string>("state");
            ltcDeclaration.NearestRlyStation = row.Field<string>("nearestRlyStation");
            ltcDeclaration.ReasonOneForDeclaration = row.Field<string>("reasonOneForDeclaration");
            ltcDeclaration.ReasonTwoForDeclaration = row.Field<string>("ReasonTwoForDeclaration");
        }
        else
        {
            AUserSession.Current.Warning = "Invalid User";
        }
    }

    public void fetchLtcDependents()
    {
        query = "SELECT * from LtcDependents where userId='" + AUserSession.Current.ThisUser.UserId + "'";
        Database.executeQuery(query);

        ltcDependentsList = new List<LtcDependents>();

        if (Database.getDataTable().Rows.Count > 0)
        {
            LtcDependents ltcDependents;
            foreach (DataRow row in Database.getDataTable().Rows)
            {
                ltcDependents = new LtcDependents();
                ltcDependents.Name = row.Field<string>("name");
                ltcDependents.Relationship = row.Field<string>("relationship");
                ltcDependents.Dob = row.Field<DateTime>("dob");
                ltcDependents.EmploymentDetails = row.Field<string>("employmentDetails");
                ltcDependents.TotalIncome = row.Field<int>("totalIncome");

                ltcDependentsList.Add(ltcDependents);

            }
        }
        else
        {
            AUserSession.Current.Warning = "Invalid User";
        }
    }
    
}
