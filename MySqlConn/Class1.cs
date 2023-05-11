namespace MySqlConn;
using MySql.Data.MySqlClient;
using System.Reflection;
public class MySqlConnecter
{
    // static MySqlConnecter() 
    // {
    //     Assembly assember = Assembly.LoadFrom("../MySql.Data.dll");
    // }
    
    private readonly MySqlConnection _conn;
    public MySqlConnection Conn { get { return _conn; } }
    public MySqlConnecter(string loginData)
    {
        _conn = new MySqlConnection(loginData);  
    }

    public void TryConnect()
    {
        try
        {
            Conn.Open();
            Console.WriteLine("Successfully Connect.");
            Conn.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            if(Conn != null)
                ((IDisposable)Conn).Dispose();
        }
    }

    public List<int> DoSomething(string cmd)
    { 
        MySqlCommand command = new MySqlCommand(cmd, Conn);
        List<int> result = new List<int>();
        try
        {
            Conn.Open();
            MySqlDataReader reader = command.ExecuteReader();
            
            while(reader.Read())
            {
                // Console.WriteLine("(" + reader[0] + ", " + reader[1] + ")");   
                result.Add((int)reader[1]);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Conn.Close();
        }

        return result;
    }

}
