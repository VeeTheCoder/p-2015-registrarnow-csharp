function GradeChangeViewModel() {

    var GradeChangeModelObj = new GradeChangeModel();
    var StudentModelObj = new StudentModel();
    var self = this;
    var initialBind = true;
    var gradeChangeListViewModel = ko.observableArray();

    this.Initialize = function (studentId) {
        var viewModel = {
            studentId: studentId,
            courseName: ko.observable(""),
            instructorName: ko.observable(""),
            message: ko.observable(""),

            insert: function (data) {
                self.Create(data);
            }
        };

        ko.applyBindings(viewModel, document.getElementById("divGradeChangeCreate"));
    };

    this.Create = function (data) {
        var model = {
            StudentName: "",
            StudentId: data.studentId,
            CourseName: data.courseName(),
            InstructorName: data.instructorName(),
            MessageBody: data.message(),
            Status: "Pending"
        }

        StudentModelObj.GetDetail(model.StudentId, function (result) {
            model.StudentName = result.FirstName + " " + result.LastName;
            GradeChangeModelObj.Create(model, function (result) {
                if (result == "ok") {
                    alert("Create grade change request successful");
                    window.history.back(-1);
                } else {
                    alert("Error occurred");
                }
            });
        });
    };

    this.GetAll = function (studentId, isInstructor) {

        GradeChangeModelObj.GetAll(studentId, isInstructor, function (gradeChangeList) {
            gradeChangeListViewModel.removeAll();

            if (gradeChangeList) {
                for (var i = 0; i < gradeChangeList.length; i++) {
                    gradeChangeListViewModel.push({
                        id: gradeChangeList[i].GradeChangeId,
                        studentName: gradeChangeList[i].StudentName,
                        studentId: gradeChangeList[i].StudentId,
                        courseName: gradeChangeList[i].CourseName,
                        instructorName: gradeChangeList[i].InstructorName,
                        message: gradeChangeList[i].MessageBody,
                        status: gradeChangeList[i].Status
                    });
                }
            }
            if (initialBind) {
                ko.applyBindings({ viewModel: gradeChangeListViewModel }, document.getElementById("divGradeChangeList"));
                initialBind = false;
            }
        });
    };

    this.GetDetail = function (id) {

        GradeChangeModelObj.GetDetail(id, function (result) {

            var gradeChangeMessage = {
                id: id,
                studentName: result.StudentName,
                studentId: result.StudentId,
                courseName: result.CourseName,
                instructorName: result.InstructorName,
                message: result.MessageBody,
                status: result.Status,

                update: function (status) {
                    self.Update(status, this);
                }
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: gradeChangeMessage }, document.getElementById("divGradeChangeGet"));
            }
        });
    };

    this.Update = function (status, viewModel) {
        var gradeChangeModelObj = new GradeChangeModel();

        var gradeChangeData = {
            GradeChangeId: viewModel.id,
            StudentName: viewModel.studentName,
            StudentId: viewModel.studentId,
            CourseName: viewModel.courseName,
            InstructorName: viewModel.instructorName,
            MessageBody: viewModel.message,
            Status: status
        };

        gradeChangeModelObj.Update(gradeChangeData, function (message) {
            $('#divMessage').html(message);
            window.history.go(-1);
        });

    };

    this.Delete = function (id) {
        GradeChangeModelObj.Delete(id, function (result) {
            if (result == "ok") {
                alert("Grade change request successfully deleted");
                window.history.back(-1);
            } else {
                alert("Error occurred in deleting request");
            }
        });

    };
}
