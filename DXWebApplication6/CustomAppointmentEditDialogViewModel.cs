using DevExpress.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Dialogs;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.XtraScheduler;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System;

public class CustomAppointmentEditDialogViewModel : AppointmentEditDialogViewModel
{
    public CustomAppointmentEditDialogViewModel(ASPxScheduler scheduler) : base()
    {
        Scheduler = scheduler;
    }

    //[DialogFieldViewSettings(Caption = "Phone")]
    //public string Phone { get; set; }

    [DialogFieldViewSettings(Caption = "Aircraft", EditorType = DialogFieldEditorType.ComboBox)]
    public override List<object> ResourceIds { get { return base.ResourceIds; } }

    [DialogFieldViewSettings(Caption = "Destination", EditorType = DialogFieldEditorType.ComboBox)]
    public override string Location
    {
        get { return base.Location; }
        set { base.Location = value; }
    }

    [DialogFieldViewSettings(Caption = "Pilot", EditorType = DialogFieldEditorType.ComboBox)]
    public override string Subject
    {
        get { return base.Subject; }
        set { base.Subject = value; }
    }

    [DialogFieldViewSettings(Caption = "Status", EditorType = DialogFieldEditorType.ComboBox)]
    public override object LabelKey
    {
        get { return base.LabelKey; }
        set { base.LabelKey = value; }
    }

    protected ASPxScheduler Scheduler { get; private set; }

    public override void Load(AppointmentFormController appointmentController)
    {
        base.Load(appointmentController);

        SetDataItemsFor(m => m.Location, PopulateLocations);
        SetDataItemsFor(m => m.Subject, PopulatePilots);


        //UpdateResourceRelatedProperties();
        //TrackPropertyChangeFor((CustomAppointmentEditDialogViewModel m) => m.ResourceIds, UpdateResourceRelatedProperties);
    }

    void PopulatePilots(AddDataItemMethod<string> addDataItemDelegate)
    {
        foreach (Pilot pilot in Pilot.pilots)
        {
            addDataItemDelegate(pilot.pilotName, pilot.pilotID.ToString());
        }
    }
    void PopulateLocations(AddDataItemMethod<string> addDataItemDelegate)
    {
        addDataItemDelegate("Hos", "Hospital");
        addDataItemDelegate("Surgery", "Surgery");
        addDataItemDelegate("Urgent Care", "Urgent Care");
        addDataItemDelegate("Pharmacy", "Pharmacy");
    }
    //void UpdateResourceRelatedProperties()
    //{
    //    if (ResourceIds.Any())
    //    {
    //        Resource resource = Scheduler.Storage.Resources.GetResourceById(ResourceIds.First());
    //        Phone = resource.CustomFields["Phone"] as string;
    //    }
    //}
    public override void SetDialogElementStateConditions()
    {
        base.SetDialogElementStateConditions();
        SetItemVisibilityCondition(m => m.Description, false);
    }
}
//using DevExpress.Web;
//using DevExpress.Web.ASPxScheduler;
//using DevExpress.Web.ASPxScheduler.Dialogs;
//using DevExpress.Web.ASPxScheduler.Internal;
//using DevExpress.XtraScheduler;
//using System.Linq;
//using System.Collections.Generic;

//public class CustomAppointmentEditDialogViewModel : AppointmentEditDialogViewModel
//{
//    public CustomAppointmentEditDialogViewModel(ASPxScheduler scheduler) : base()
//    {
//        Scheduler = scheduler;
//    }


//    [DialogFieldViewSettings(Caption = "Doctor", EditorType = DialogFieldEditorType.ComboBox)]
//    public override List<object> ResourceIds { get { return base.ResourceIds; } }

//    [DialogFieldViewSettings(Caption = "Department", EditorType = DialogFieldEditorType.ComboBox)]
//    public override string Location
//    {
//        get { return base.Location; }
//        set { base.Location = value; }
//    }

//    protected ASPxScheduler Scheduler { get; private set; }

//    public override void Load(AppointmentFormController appointmentController)
//    {
//        base.Load(appointmentController);

//        SetDataItemsFor(m => m.Location, PopulateLocations);


//    }
//    void PopulateLocations(AddDataItemMethod<string> addDataItemDelegate)
//    {
//        addDataItemDelegate("Hospital", "Hospital");
//        addDataItemDelegate("Surgery", "Surgery");
//        addDataItemDelegate("Urgent Care", "Urgent Care");
//        addDataItemDelegate("Pharmacy", "Pharmacy");
//    }

//    public override void SetDialogElementStateConditions()
//    {
//        base.SetDialogElementStateConditions();
//        SetItemVisibilityCondition(m => m.Description, false);
//    }
//}