using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraScheduler;


public class SchedulerAppointment
{
    public int ID { get; set; }
    public int ResourceID { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Label { get; set; }
    public string Status { get; set; }
    public string MyRecurrenceInfo { get; set; }
    public int Type { get; set; }
    public string Description { get; set; }
}
public class AppointmentDataSourceHelper
{
    public BindingList<SchedulerAppointment> Read()
    {
        return AppointmentsData;
    }
    public int Create(SchedulerAppointment postedItem)
    {
        
        Console.WriteLine(postedItem.ResourceID);
        int newID = 0;
        foreach (SchedulerAppointment item in AppointmentsData)
            if (newID < item.ID)
                newID = item.ID;
        newID = newID + 1;
        AppointmentsData[AppointmentsData.Count - 1].ID = newID;
        return newID;
    }
    public void Update(SchedulerAppointment postedItem)
    {
        var editedItem = AppointmentsData.First(i => i.ID == postedItem.ID);
        LoadNewValues(editedItem, postedItem);
    }
    public void Delete(SchedulerAppointment deletedItem)
    {
        var item = AppointmentsData.First(i => i.ID == deletedItem.ID);
        AppointmentsData.Remove(item);
    }
    protected void LoadNewValues(SchedulerAppointment newItem, SchedulerAppointment postedItem)
    {
        newItem.Label = postedItem.Label;
        newItem.EndDate = postedItem.EndDate;
        newItem.MyRecurrenceInfo = postedItem.MyRecurrenceInfo;
        newItem.ResourceID = postedItem.ResourceID;
        newItem.StartDate = postedItem.StartDate;
        newItem.Status = postedItem.Status;
        newItem.Title = postedItem.Title;
        newItem.Type = postedItem.Type;
        newItem.Description = postedItem.Description;
    }
    private BindingList<SchedulerAppointment> AppointmentsData
    {
        get
        {
            var key = "34FAA431-CF79-4869-9488-93F6AAE81263";
            var Session = HttpContext.Current.Session;
            if (Session[key] == null)
                Session[key] = GenerateDS();
            return (BindingList<SchedulerAppointment>)Session[key];
        }
    }
    public BindingList<SchedulerAppointment> GenerateDS()
    {
        BindingList<SchedulerAppointment> model = new BindingList<SchedulerAppointment>();
        return model;
    }
}
public class SchedulerResource
{
    public int ResourceID { get; set; }
    public string ResourceName { get; set; }
}
public class ResourceDataSourceHelper
{
    public BindingList<SchedulerResource> GetDS()
    {

        //resourceID->aircraft regno
        BindingList<SchedulerResource> model = new BindingList<SchedulerResource>();
        model.Add(new SchedulerResource() { ResourceID = 1, ResourceName = "Aircraft 1" });
        model.Add(new SchedulerResource() { ResourceID = 2, ResourceName = "Aircraft 2" });
        model.Add(new SchedulerResource() { ResourceID = 3, ResourceName = "Aircraft 3" });

        return model;
    }
}
public class Pilot
{
    public int pilotID { get; set; }
    public string pilotName { get; set; }
    public string pilotShortName { get; set; }
    public bool validFlyingLisence { get; set; }
    //public string Label { get; set; }
    public static BindingList<Pilot> pilots = new BindingList<Pilot>();


    public static void GeneratePilots()
    {
        BindingList<Pilot> model = new BindingList<Pilot>();
        model.Add(new Pilot() { pilotID = 1, pilotName = "Delayed", pilotShortName = "App1", validFlyingLisence = true });
        model.Add(new Pilot() { pilotID = 2, pilotName = "Answered", pilotShortName = "App2", validFlyingLisence = false });

        pilots = model;
    }


}