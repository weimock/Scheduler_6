using System;
using DevExpress.Web.Internal;
using DevExpress.XtraScheduler;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web;

namespace DXWebApplication6
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Pilot.GeneratePilots();
            ASPxGridView1.DataSource = Pilot.pilots;
            ASPxGridView1.DataBind();
            ASPxScheduler1.Start = DateTime.Today;
            ASPxScheduler1.Storage.Appointments.Labels.Clear();
            ASPxScheduler1.Storage.Appointments.Labels.Add("Confirmed", "Confirmed", "MenuCaptionConfirmed", System.Drawing.Color.Green);
            ASPxScheduler1.Storage.Appointments.Labels.Add("Pending", "Pending", "MenuCaptionPending", System.Drawing.Color.Red);

            ASPxScheduler1.Storage.Appointments.Statuses.Clear();
            //ASPxScheduler1.Storage.Appointments.Statuses.Add("Confirmed", AppointmentStatusType.Custom, "Confirmed", "MenuCaptionConfirmed", System.Drawing.Color.Yellow);
            //ASPxScheduler1.Storage.Appointments.Statuses.Add("Pending", AppointmentStatusType.Custom, "Pending", "MenuCaptionPending", System.Drawing.Color.Blue);

            PrepareAppointmentDialog();
        }

        protected void ASPxGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                //e.Row.Attributes.Add("appTitle", e.GetValue("Title").ToString());
                //e.Row.Attributes.Add("appLabel", e.GetValue("Label").ToString());
                //e.Row.Attributes.Add("appStatus", e.GetValue("Status").ToString());
                //e.Row.Attributes.Add("appDuration", e.GetValue("DurationInHours").ToString());
                e.Row.Attributes.Add("pilotID", e.GetValue("pilotID").ToString());
                e.Row.Attributes.Add("pilotName", e.GetValue("pilotName").ToString());
                e.Row.Attributes.Add("pilotShortName", e.GetValue("pilotShortName").ToString());
                e.Row.Attributes.Add("validFlyingLisence", e.GetValue("validFlyingLisence").ToString());
            }
        }

        protected void ASPxScheduler1_HtmlTimeCellPrepared(object handler, DevExpress.Web.ASPxScheduler.ASPxSchedulerTimeCellPreparedEventArgs e)
        {
            //if (e.View.Type != SchedulerViewType.Day) return;
            e.Cell.CssClass += " myDroppableClass";
            e.Cell.Attributes.Add("intResource", e.Resource.Id.ToString());
            e.Cell.Attributes.Add("intStart", e.Interval.Start.ToString());
        }

        protected void ASPxScheduler1_CustomCallback(object sender, CallbackEventArgsBase e)
        {
            var scheduler = (ASPxScheduler)sender;
            string[] parameters = e.Parameter.Split('|');
            Appointment newApp = scheduler.Storage.Appointments.CreateAppointment(AppointmentType.Normal);
            newApp.Subject = parameters[0];
            newApp.ResourceId = parameters[1];
            newApp.StatusKey = parameters[2];
            newApp.LabelKey = parameters[3];
            newApp.Start = DateTime.Parse(parameters[4]);
            newApp.End = DateTime.Parse(parameters[4]).AddHours(1.00);
            scheduler.Storage.Appointments.Add(newApp);
            scheduler.ActiveView.SelectAppointment(newApp);
            scheduler.Storage.RefreshData();
        }

        void PrepareAppointmentDialog()
        {
            var dialog = ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog;
            dialog.ViewModel = new CustomAppointmentEditDialogViewModel(ASPxScheduler1);
            dialog.GenerateDefaultLayoutElements();

            dialog.FindLayoutElement("ResourceIds").ColSpan = 1;
            //dialog.InsertAfter(
            //    dialog.LayoutElements.CreateField((CustomAppointmentEditDialogViewModel m) => m.Phone),
            //    dialog.FindLayoutElement("ResourceIds")
            //);

            dialog.RemoveLayoutElement("Reminder");
            dialog.RemoveLayoutElement("IsAllDay");
            dialog.RemoveLayoutElement("StatusKey");
            //var dialog = ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog;
            //dialog.ViewModel = new CustomAppointmentEditDialogViewModel(ASPxScheduler1);
            //dialog.GenerateDefaultLayoutElements();

            //dialog.FindLayoutElement("ResourceIds").ColSpan = 1;


        }

        //protected void ASPxScheduler1_AppointmentFormShowing(object sender, DevExpress.Web.ASPxScheduler.AppointmentFormEventArgs e)
        //{
        //    e.Container.Caption = e.Container.IsNewAppointment ? "Create New Appointment" : "Edit Appointment: " + e.Appointment.Subject;
        //}

        protected void ASPxSchedulerControl1_InitAppointmentDisplayText(object sender, AppointmentDisplayTextEventArgs e)
        {
  
            //apt.subject is pilotID, either call stored procedure to find pilot name or search locally
                Appointment apt = e.Appointment;
            Console.WriteLine(apt.Location);
            if(apt.Location == "")
            {
                e.Text = String.Format("{0}", apt.Subject);
            }
            else
            {
                e.Text = String.Format("[{0}] {1}", apt.Location, apt.Subject);
                //e.Description = String.Format("Details: {0}", apt.Description);
            }


        }
    }
}