<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebForm1.aspx.cs" Inherits="DXWebApplication6.WebForm1" %>

<%@ Register Assembly="DevExpress.Web.v20.2, Version=20.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v20.2, Version=20.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>

<%@ Register Assembly="DevExpress.XtraScheduler.v20.2.Core, Version=20.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v20.2.Core.Desktop, Version=20.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.10.4/jquery-ui.min.js"></script>
    <style>
        .activeHover {
            background-color: lightblue !important;
        }

        .ui-draggable-dragging {
            background-color: lightgreen;
            color: White;
        }

        .pilotAlign{
            display:inline; 
            float:left; 
/*            width:30%;*/
/*            margin-left:50px;
            margin-top:50px;*/
           padding-left:20px;
           padding-top: 20px;

                
        }

        .shAlign
        {
            display:inline;
            float:right;
            width:70%;
        }

        .calSize{
            height:50px; 
        }
    </style>
    <script type="text/javascript">
        function InitalizejQuery() {
            $('.draggable').draggable({
                helper: 'clone',
                start: function (ev, ui) {
                    var $draggingElement = $(ui.helper);
                    $draggingElement.width(gridView.GetWidth());
                }
            });
            $('.myDroppableClass').droppable({
                tolerance: "intersect",
                hoverClass: "activeHover",
                drop: function (event, ui) {
                    var appTitle = ui.draggable.attr("pilotName");
                    var appStatus = ui.draggable.attr("pilotShortName");
                    var appLabel = ui.draggable.attr("pilotName");
                    var intResource = $(this).attr("intResource");
                    var intStart = $(this).attr("intStart");
                    scheduler.PerformCallback(appTitle + "|" + intResource + "|" + appStatus + "|" + appLabel + "|" + intStart);
                }
            });

            function onLeftPanelInit(s, e) {
                var adjustmentMethod = function () {
                    var pageToolbarPanel = ASPx.GetControlCollection().GetByName("pageToolbarPanel");
                    if (pageToolbarPanel)
                        pageToolbarPanel.GetMainElement().style.left = s.GetWidth() + "px";

                    var toggleButton = leftAreaMenu.GetItemByName("ToggleLeftPanel");
                    if (s.IsExpandable())
                        toggleButton.SetChecked(leftPanel.IsExpanded());
                    else {
                        if (leftPanel.GetVisible())
                            document.body.style.marginLeft = "1px";
                        else
                            document.body.style.marginLeft = 0;
                        toggleButton.SetChecked(leftPanel.GetVisible());
                    }
                };
                AddAdjustmentDelegate(adjustmentMethod);
            }

            function onLeftPanelCollapsed(s, e) {
                leftAreaMenu.GetItemByName("ToggleLeftPanel").SetChecked(false);
            }
            window.onLeftPanelInit = onLeftPanelInit;
            window.onLeftPanelCollapsed = onLeftPanelCollapsed;

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display:inline-block">
<%--              <dx:ASPxPanel runat="server" ID="LeftPanel" ClientInstanceName="leftPanel"
                Collapsible="true" ScrollBars="Auto" FixedPosition="WindowLeft" Width="272px"
                CssClass="left-panel" Paddings-Padding="0" Styles-ExpandBar-CssClass="expand-bar">
                <SettingsAdaptivity CollapseAtWindowInnerWidth="960" />
                <SettingsCollapsing ExpandButton-Visible="false" ExpandEffect="PopupToRight" AnimationType="Slide" Modal="true" />

                <ClientSideEvents Init="onLeftPanelInit" Collapsed="onLeftPanelCollapsed" />
            </dx:ASPxPanel>--%>
            <div class="pilotAlign">
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="pilotID"
                 ClientInstanceName="gridView"  onHtmlRowPrepared="ASPxGridView1_HtmlRowPrepared" Settings-GridLines="Vertical" >
                    <Settings GridLines="Vertical" />
<SettingsPopup>
<FilterControl AutoUpdatePosition="False"></FilterControl>
</SettingsPopup>
                <Columns>
            <%--        <dx:GridViewDataTextColumn FieldName="pilotID" ReadOnly="True" VisibleIndex="0">
                    </dx:GridViewDataTextColumn>--%>
                    <dx:GridViewDataTextColumn FieldName="pilotName" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="pilotShortName" VisibleIndex="2">
                    </dx:GridViewDataTextColumn>
<%--                    <dx:GridViewDataCheckColumn FieldName="validFlyingLisence" VisibleIndex="3">
                    </dx:GridViewDataCheckColumn>--%>
                </Columns>
                <Styles>
                    <Row CssClass="draggable"></Row>
                </Styles>
    </dx:ASPxGridView>

            </asp:Panel>
            </div>
     
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="pilots" TypeName="Pilot"></asp:ObjectDataSource>

            <div class="shAlign">
            <asp:Panel ID="Panel2" runat="server">

            <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" Width="107%" AppointmentDataSourceID="ObjectDataSourceAppointments" ClientInstanceName="scheduler"
                ResourceDataSourceID="ObjectDataSourceResources" GroupType="Resource" OnHtmlTimeCellPrepared="ASPxScheduler1_HtmlTimeCellPrepared"
                OnCustomCallback="ASPxScheduler1_CustomCallback" CssClass="shAlign" OnInitAppointmentDisplayText="ASPxSchedulerControl1_InitAppointmentDisplayText">
                <Storage>
                    <Appointments AutoRetrieveId="true">
                        <Mappings AppointmentId="ID" End="EndDate" Label="Label" ResourceId="ResourceID" Start="StartDate" Status="Status" Subject="Title" RecurrenceInfo="MyRecurrenceInfo" Type="Type" Description="Description" />
                    </Appointments>
                    <Resources>
<%--                         <CustomFieldMappings>
                             <dxwschs:ASPxResourceCustomFieldMapping Name="Phone" Member="Phone" />
                         </CustomFieldMappings>--%>
                        <Mappings Caption="ResourceName" ResourceId="ResourceID" />
                    </Resources>

                </Storage>
                <Views>
                    <DayView>
                        <TimeRulers>
                            <cc1:TimeRuler></cc1:TimeRuler>
                        </TimeRulers>
                        <AppointmentDisplayOptions ColumnPadding-Left="2" ColumnPadding-Right="4"></AppointmentDisplayOptions>
                        <DayViewStyles ScrollAreaHeight="300px"></DayViewStyles>
                    </DayView>
                    <WorkWeekView ViewSelectorItemAdaptivePriority="6">
                        <TimeRulers>
                            <cc1:TimeRuler></cc1:TimeRuler>
                        </TimeRulers>
                        <AppointmentDisplayOptions ColumnPadding-Left="2" ColumnPadding-Right="4"></AppointmentDisplayOptions>
                    </WorkWeekView>
                    <WeekView Enabled="false"></WeekView>
                    <MonthView ViewSelectorItemAdaptivePriority="5"></MonthView>
                    <TimelineView ViewSelectorItemAdaptivePriority="3"></TimelineView>
                    <FullWeekView Enabled="true">
                        <TimeRulers>
                            <cc1:TimeRuler></cc1:TimeRuler>
                        </TimeRulers>
                        <AppointmentDisplayOptions ColumnPadding-Left="2" ColumnPadding-Right="4"></AppointmentDisplayOptions>
                    </FullWeekView>
                    <AgendaView DayHeaderOrientation="Auto" ViewSelectorItemAdaptivePriority="1"></AgendaView>
                </Views>
                <OptionsToolTips AppointmentToolTipCornerType="None"></OptionsToolTips>
            </dxwschs:ASPxScheduler>
                </asp:Panel>
                </div>
            </div>

            <dx:ASPxGlobalEvents ID="ge" runat="server">
                <ClientSideEvents ControlsInitialized="InitalizejQuery" EndCallback="InitalizejQuery" />
            </dx:ASPxGlobalEvents>
            <asp:ObjectDataSource ID="ObjectDataSourceAppointments" runat="server"
                DataObjectTypeName="SchedulerAppointment" TypeName="AppointmentDataSourceHelper"
                InsertMethod="Create" UpdateMethod="Update" SelectMethod="Read" DeleteMethod="Delete" />
            <asp:ObjectDataSource ID="ObjectDataSourceResources" runat="server"
                DataObjectTypeName="SchedulerResource" TypeName="ResourceDataSourceHelper"
                SelectMethod="GetDS" />
       
    </form>
</body>
</html>
