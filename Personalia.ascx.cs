using System;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using nnn.Framework.BE;
using nnn.Framework.BL;
using nnn.Framework.UI;
using System.Linq;
using nnn.Framework.Base;

public partial class UserControls_Personalia : System.Web.UI.UserControl
{
    string pid;
    byte[] SelectedUser;

    protected void Page_Load(object sender, EventArgs e)
    {       
        pid = HttpContext.Current.Request.QueryString["pid"].ToString();
        SelectedUser = Pris.Global.Helpers.GetUserKey(pid);
        if (!Page.IsPostBack)
        {
            INIT_RECHTEN_LOOKUP();
            Retrieve(SelectedUser, Convert.ToInt32(pid), 0);            
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    private void Retrieve(byte[] SelectedUser, int pid, int _SelectedPid)
    {
        Personalia _Personalia = null;
        if (_SelectedPid == 0)
            _Personalia = new PersonaliaBL().RetrieveSingleRecord(SelectedUser, pid);
        else
            _Personalia = new PersonaliaBL().RetrieveSingleRecord(SelectedUser, pid, _SelectedPid);

        UIDataBinding.ControlBinder(this.Controls, _Personalia);

        P_HistoryChangesList.Visible = (Pris.Global.Helpers.isUserBeheerder());
    }

    private PasFoto UploadPasFoto(byte[] userid)
    {
        PasFoto _Foto = new PasFoto();
        if (FU_PasFoto.FileUploadVisible)
        {
            if ((FU_PasFoto.PostedFile != null) && (FU_PasFoto.PostedFile.ContentLength > 0))
            {
                byte[] f = new byte[FU_PasFoto.PostedFile.InputStream.Length];
                FU_PasFoto.PostedFile.InputStream.Read(f, 0, Convert.ToInt32(FU_PasFoto.PostedFile.InputStream.Length));
                FU_PasFoto.PostedFile.InputStream.Close();

                _Foto.Foto = f;
                _Foto.UserId = userid;
                _Foto.FotoType = FU_PasFoto.PostedFile.ContentType.ToString();
            }
        }
        return _Foto;
    }

    public void Save()
    {
        int New_Pid = 0;
        try 
        {
            // Eerst mapping naar huidige object
            using (Personalia _Personalia = new PersonaliaBL().RetrieveSingleRecord(SelectedUser, Convert.ToInt32(pid)))
            {
                // Mapping naar nieuwe Object met nieuwe waarde
                UIDataBinding.ObjectBinder(this.Controls, _Personalia);

                // klein beetje maatwerk
                using(PasFoto _PasFoto = UploadPasFoto(_Personalia.Userid))
                {
                    _Personalia.PasFoto = _PasFoto;
                    _Personalia.Foto = _PasFoto.Foto;
                }

                // nieuwe Object opslaan
                New_Pid = new PersonaliaBL().Save(_Personalia);
            }
        }
        catch (Exception ex)
        {
            Pris.Global.Helpers.ToonBericht(ex.Message);
        }

        if (New_Pid > 0)
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.ServerVariables["url"] + "?pid=" + New_Pid + "&tid=" + HttpContext.Current.Request.QueryString["tid"].ToString());
    }

    private void INIT_RECHTEN_LOOKUP()
    {
        if (!Pris.Global.Helpers.isUserBeheerder())
        {
            string[] userRoles = Roles.GetRolesForUser();
            RechtenLoopUpList<RechtenLoopUp> _RechtenLoopUpList = RechtenLoopUpBL.RetrieveRechtenLoopUpList(EnumCollection.TableType.Personalia);
            UIDataBinding.InitControlPermission<Personalia>(this.Controls, new Personalia(), _RechtenLoopUpList, userRoles.ToList());
        }
    }

    protected void lbtnDeleteFoto_Click(object sender, EventArgs e)
    {
        Personalia _Personalia = new PersonaliaBL().RetrieveSingleRecord(SelectedUser, Convert.ToInt32(pid));

        new PersonaliaBL().DeletePasFoto(_Personalia);
        Pris.Global.Helpers.ReloadCurrentPage();
    }

    protected void P_HistoryChangesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string SelectedPid = P_HistoryChangesList.GekozenWaarde;

        // zet default waarde op gekozende waarde!
        P_HistoryChangesList.DefaultWaarde = SelectedPid;
        INIT_RECHTEN_LOOKUP();
        Retrieve(SelectedUser, Convert.ToInt32(pid), Convert.ToInt32(SelectedPid));        
    }
}
