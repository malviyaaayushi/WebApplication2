using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public class UserFuncs
{
    private string sdwConnectionString = @"Data Source = .\SQLExpress; user id=sa; password=server; Initial Catalog = oap.v.02.2;";
    private SqlConnection sdwDBConnection;  // to create a connection 
    private string query; //to hold the query
    private SqlCommand queryCommand; //pass the constructor the connection string and the query string in this
    private SqlDataReader queryCommandReader; 
    private DataTable dataTable; //to hold all the data returned by the query.
    private User user = null; 
    private static string PASSCODE = "ffpdf_p1w2d";
    private static byte[] SALT = new byte[] { 0xAE, 0x16, 0xAE, 0x5e, 0x12, 0x32, 0x23, 0xEF, 0xAB, 0x41, 0x66, 0x69, 0xCE };
    private static PasswordDeriveBytes MY_KEY = new PasswordDeriveBytes(PASSCODE, SALT);
    private static byte[] MY_KEY32 = MY_KEY.GetBytes(32);
    private static byte[] MY_KEY16 = MY_KEY.GetBytes(16);
    public List<Application> boxContent;
 
    public User loginUser(String userName, String password)
    {
        try
        {
            connectToDatabse();
            query = "SELECT * from LoginCredentials where userName='" + userName+"'";
            queryCommand = new SqlCommand(query, sdwDBConnection);
            queryCommandReader = queryCommand.ExecuteReader();
            dataTable = new DataTable();

            dataTable.Load(queryCommandReader);

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                String hashPassword = ComputeHash(password, "", null); //row.Field<String>("pswd");
                if (VerifyHash(password, "", hashPassword))
                {
                    user = new User();
                    user.UserId = row.Field<int>("userId");
                    user.UserName = row.Field<String>("userName");
                    user.ActorRank = row.Field<int>("actorRank");
                    user.Type = row.Field<int>("type");
                    user.Designation = row.Field<String>("designation");
                    user.Name = row.Field<String>("name");
                    return user;
                }
                else {
                    AUserSession.Current.Warning = "Invalid Username-Password combination";
                }
            }
            else {
                AUserSession.Current.Warning = "Invalid User";
            }
            disconnectToDatabase();
        }
        catch (Exception ex)
        {
            AUserSession.Current.Warning = ex.Message;// "Unable to connect to database";
        }        
        return null;
    }

    public void giveAccessToUser(User user) {
        AUserSession.Current.ThisUser = user;
        AUserSession.Current.removeAllWarnings();
        System.Web.HttpContext.Current.Response.Redirect("Default.aspx");
    }

    public void loadUserBox(String box_type)
    {
        try
        {
            connectToDatabse();
            if (box_type.ToLower().Equals("trash"))
            {
                query = "SELECT * from DeletedApplications where applicantId='" + AUserSession.Current.ThisUser.UserId + "'";
            }
            else if (box_type.ToLower().Equals("outbox"))
            {
                query = "SELECT * from LeaveDetails where applicantId='" + AUserSession.Current.ThisUser.UserId + "'";
            }
            else if (box_type.ToLower().Equals("important"))
            {
                query = "SELECT * from LeaveDetails where applicantId='" + AUserSession.Current.ThisUser.UserId + "'";
            }
            else
            {
                query = "SELECT * from LeaveDetails where recommAuthId='" + AUserSession.Current.ThisUser.UserId + "' OR approvAuthId=" + AUserSession.Current.ThisUser.UserId;
            }            
            executeQuery();

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = null;
                boxContent = new List<Application>();
                for (int i = 0; i < dataTable.Rows.Count; i++) {
                    row = dataTable.Rows[i];
                    Application application = new Application();
                    application.ApplicantId = row.Field<int>("applicantId");
                    application.ApplicantName = row.Field<String>("applicantName");
                    application.ApplicationId = row.Field<int>("applicationId");
                    application.RecommAuthId = row.Field<int>("recommAuthId");
                    application.ApprovAuthId = row.Field<int>("approvAuthId");
                    application.ApplicationType = row.Field<int>("applicationType");
                    boxContent.Add(application);
                }                
            }
            disconnectToDatabase();
        }
        catch (Exception ex)
        {
            AUserSession.Current.Warning = ex.Message;
        } 
    }

    /*
     * Helper Functions
     * */

    public static bool VerifyHash(string plainText, string hashAlgorithm, string hashValue)
    {

        // Convert base64-encoded hash value into a byte array.
        byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

        // We must know size of hash (without salt).
        int hashSizeInBits, hashSizeInBytes;

        // Make sure that hashing algorithm name is specified.
        if (hashAlgorithm == null)
            hashAlgorithm = "";

        // Size of hash is based on the specified algorithm.
        switch (hashAlgorithm.ToUpper())
        {

            case "SHA384":
                hashSizeInBits = 384;
                break;

            case "SHA512":
                hashSizeInBits = 512;
                break;

            default: // Must be MD5
                hashSizeInBits = 128;
                break;
        }

        // Convert size of hash from bits to bytes.
        hashSizeInBytes = hashSizeInBits / 8;

        // Make sure that the specified hash value is long enough.
        if (hashWithSaltBytes.Length < hashSizeInBytes)
            return false;

        // Allocate array to hold original salt bytes retrieved from hash.
        byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

        // Copy salt from the end of the hash to the new array.
        for (int i = 0; i < saltBytes.Length; i++)
            saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

        // Compute a new hash string.
        string expectedHashString = ComputeHash(plainText, hashAlgorithm, saltBytes);

        // If the computed hash matches the specified hash,
        // the plain text value must be correct.
        return (hashValue == expectedHashString);
    }

    public static string ComputeHash(string plainText, string hashAlgorithm, byte[] saltBytes)
    {
        /*
         * used while registering the user
         * */
        // If salt is not specified, generate it.
        if (saltBytes == null)
        {
            // Define min and max salt sizes.
            int minSaltSize = 4;
            int maxSaltSize = 8;

            // Generate a random number for the size of the salt.
            Random random = new Random();
            int saltSize = random.Next(minSaltSize, maxSaltSize);

            // Allocate a byte array, which will hold the salt.
            saltBytes = new byte[saltSize];

            // Initialize a random number generator.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            // Fill the salt with cryptographically strong byte values.
            rng.GetNonZeroBytes(saltBytes);
        }

        // Convert plain text into a byte array.
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        // Allocate array, which will hold plain text and salt.
        byte[] plainTextWithSaltBytes =
        new byte[plainTextBytes.Length + saltBytes.Length];

        // Copy plain text bytes into resulting array.
        for (int i = 0; i < plainTextBytes.Length; i++)
            plainTextWithSaltBytes[i] = plainTextBytes[i];

        // Append salt bytes to the resulting array.
        for (int i = 0; i < saltBytes.Length; i++)
            plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

        HashAlgorithm hash;

        // Make sure hashing algorithm name is specified.
        if (hashAlgorithm == null)
            hashAlgorithm = "";

        // Initialize appropriate hashing algorithm class.
        switch (hashAlgorithm.ToUpper())
        {

            case "SHA384":
                hash = new SHA384Managed();
                break;

            case "SHA512":
                hash = new SHA512Managed();
                break;

            default:
                hash = new MD5CryptoServiceProvider();
                break;
        }

        // Compute hash value of our plain text with appended salt.
        byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

        // Create array which will hold hash and original salt bytes.
        byte[] hashWithSaltBytes = new byte[hashBytes.Length +
        saltBytes.Length];

        // Copy hash bytes into resulting array.
        for (int i = 0; i < hashBytes.Length; i++)
            hashWithSaltBytes[i] = hashBytes[i];

        // Append salt bytes to the result.
        for (int i = 0; i < saltBytes.Length; i++)
            hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

        // Convert result into a base64-encoded string.
        string hashValue = Convert.ToBase64String(hashWithSaltBytes);

        // Return the result.
        return hashValue;
    }

    private void connectToDatabse()
    {
        sdwDBConnection = new SqlConnection(sdwConnectionString);
        sdwDBConnection.Open();
    }

    private void executeQuery()
    {
        queryCommand = new SqlCommand(query, sdwDBConnection);
        queryCommandReader = queryCommand.ExecuteReader();
        dataTable = new DataTable();
        dataTable.Load(queryCommandReader);
    }

    private void disconnectToDatabase()
    {
        sdwDBConnection.Close();
    }

}
