function ScheduleViewModel() {
    var initialBind = true;

    this.Create= function (data) {
        var scheduleModelObj = new ScheduleModel();

        var model = {
            Course: { CourseId: data.courseid() },
            Year: data.year(),
            Quarter: data.quarter(),
            Session: data.session()
        }

        scheduleModelObj.Create(model, function (result) {
            if (result == "ok") {
                alert("Create schedule successful");
            } else {
                alert("Error occurred");
            }
        });
    };

    this.ManageScheduleLoad = function () {
        var scheduleModelObj = new ScheduleListModel();

        scheduleModelObj.Load("","",function (scheduleListData) {

            //process array and return observable
            var scheduleViewModel = processManageView(scheduleListData);

            if (initialBind) {
                ko.applyBindings({ viewModel: scheduleViewModel }, document.getElementById("divManageScheduleContent"));
            }
        });
    };
}
