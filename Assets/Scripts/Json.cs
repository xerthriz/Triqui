using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;

public class Json : MonoBehaviour
{
    public string url=null;
    public UILabel Id;
    public UILabel Users;
    public UILabel Game;
    private string Name;
    private string Username;
    private string Email;
    private string Addresss;
    private string Geos;
    private string phone;
    private string website;
    private string Companys;
    // Sample JSON for the following script has attached.
    IEnumerator Start()
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            Processjson(www.data);
          
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }
    
    }

    private void Processjson(string jsonString)
    {
         JSONObject j = new JSONObject(jsonString);
         int range=Random.Range(0,j.list.Count);
         JSONObject User = j.list[range];
         JSONObject Address = User.list[4];
         JSONObject Geo = Address.list[4];
         JSONObject Company = User.list[7];


       
             Id.text = "Id: " + User.list[0].ToString();
             Name = "Name: " + User.list[1].ToString() + " \n";
             Username = "Username: " + User.list[2].ToString() + " \n";
             Email = "Email:" + User.list[3].ToString() + " \n";
             Addresss = "Street: " + Address.list[0].ToString() + " \n" + "Suite: " + Address.list[1].ToString() + " \n" + "City: " + Address.list[2].ToString() + " \n" + "ZipCode: " + Address.list[3].ToString() + " \n";
             Geos = "Lat: " + Geo.list[0].ToString() + "Lng: " + Geo.list[1].ToString() + " \n";
             phone = "Phone: " + User.list[5].ToString() + " \n";
             website = "Website: " + User.list[6].ToString() + " \n";
             Companys = "Name: " + Company.list[0].ToString() + " \n" + "CatchPhrase: " + Company.list[1].ToString() + " \n" + "Bs: " + Company.list[2].ToString() + " \n";
             Users.text = Name + Username + Email + "\n\nAddress\n\n" + Addresss + "\n\nGeo\n\n" + Geos + phone + website + "\n\nCompany\n\n" + Companys;
             Game.text = "Mr." + User.list[1].ToString();
      
      }

}