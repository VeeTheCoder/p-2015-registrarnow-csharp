function EnrollmentViewModel() {
    var enrolledList = ko.observableArray();

    this.Create = function (data, callback) {
        var enrollmentModelObj = new EnrollmentModel();

        var model = {
            StudentId: data.studentId(),
            ScheduleId: data.scheduleId(),
            Grade: data.grade(),
            GradeValue: data.gradeValue()
        }

        enrollmentModelObj.Create(model, function (result) {
            if (result == "ok") {
                alert("Create schedule successful.");
                callback(data.studentId());
            } else {
                alert("You are already enrolled in this section.");
            }
        });
    };

    this.Delete = function (studentid, scheduleid) {
        var enrollmentModelObj = new EnrollmentModel();

        enrollmentModelObj.Delete(studentid, scheduleid, function (result) {
            if (result == "ok") {
                alert("Delete successful.");
            } else {
                alert("Delete failed.");
            }
        });
    }

    function processSchedule(obj, id) {
        //append to object a start initializer
        obj.started = false;
        obj.deleted = false; //delete flag
        obj.Rank = ko.observable();
        var rankingModelObj = new RankingsModel();
        rankingModelObj.Get(obj.ScheduleId,id, function (result) {
            if(result){
                obj.Rank(result.Rank);
            } else {
                obj.Rank("None");
            }
        });

        obj.handleClick = function () {
            if (this.started) {
                //delete is clicked
                var enrollmentViewModelObj = new EnrollmentViewModel();

                enrollmentViewModelObj.Delete(id, this.ScheduleId);
                obj.deleted = true;
                enrolledList.remove(function (item) { return item.deleted });
            }
            this.started = true;
        }
        return obj;
    }

    this.Load = function (year, quarter, id) {
        var scheduleModelObj = new ScheduleModel();

        scheduleModelObj.Load(year, quarter, function (scheduleListData) {
            var scheduleViewModel = {
                data: ko.observableArray(),
                selectable: ko.observable(),
                handleClick: function () {
                    if (this.selectable()) {
                        var enrollmentViewModelObj = new EnrollmentViewModel();

                        var passFunction = function (id) {
                            var studentModelObj = new StudentModel();
                            studentModelObj.GetDetail(id, function (result) {
                                enrolledList.removeAll();
                                for (var i = 0; i < result.Enrolled.length; i++) {
                                    enrolledList.push(processSchedule(result.Enrolled[i], id));
                                }
                            });
                        };

                        enrollmentViewModelObj.Create({
                            studentId: ko.observable(this.selectable().studentid),
                            scheduleId: ko.observable(this.selectable().scheduleid),
                            grade: ko.observable(null),
                            gradeValue: ko.observable(0.0)
                        }, passFunction);
                    }
                }
            };

            if (scheduleListData) {
                for (var i = 0; i < scheduleListData.length; i++) {
                    scheduleViewModel.data.push({
                        studentid: id,
                        scheduleid: scheduleListData[i].ScheduleId,
                        title: scheduleListData[i].Course.Title,
                        description: scheduleListData[i].Course.Description,
                        year: scheduleListData[i].Year,
                        quarter: scheduleListData[i].Quarter,
                        session: scheduleListData[i].Session
                    });
                }
            }

            // this is using knockoutjs to bind the viewModel and the view (Home/Index.cshtml)
            ko.applyBindings({ viewModel: scheduleViewModel }, document.getElementById("divScheduleContent"));
        });

        var StudentModelObj = new StudentModel();
        StudentModelObj.GetDetail(id, function (result) {
            for (var i = 0; i < result.Enrolled.length; i++) {
                enrolledList.push(processSchedule(result.Enrolled[i], id));
            }

            ko.applyBindings({ enrolledListView: enrolledList }, document.getElementById("divEnrollmentContent"));
        });
    };
}
