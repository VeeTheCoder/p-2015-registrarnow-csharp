function AcademicAdvisingViewModel() {

    var AcademicAdvisingModelObj = new AcademicAdvisingModel();
    var StudentModelObj = new StudentModel();
    var self = this;
    var initialBind = true;
    var academicAdvisingListViewModel = ko.observableArray();

    this.Initialize = function (sid) {
        var viewModel;
        StudentModelObj.GetDetail(sid, function (res) {
            viewModel = {

                studentId: ko.observable(sid),
                messageSubject: ko.observable(""),
                messageBody: ko.observable(""),
                sendToInstructor: true,
                studentName: ko.observable(res.FirstName + " " + res.LastName),
                create: function (data) {
                    self.Create(data);
                }

            };

            ko.applyBindings(viewModel, document.getElementById("divAcademicAdvisingCreate"));

        });
    };

    this.Initialize2 = function () {
        var viewModel;
        StudentModelObj.GetDetail(sid, function (res) {
            viewModel = {

                studentId: ko.observable(sid),
                messageSubject: ko.observable(""),
                messageBody: ko.observable(""),
                sendToInstructor: true,
                studentName: ko.observable(res.FirstName + " " + res.LastName),
                create: function (data) {
                    self.Create(data);
                }

            };

            ko.applyBindings(viewModel, document.getElementById("divAcademicAdvisingCreate"));

        });
    };

    this.GetMessage = function (id) {
        AcademicAdvisingModelObj.GetMessage(id, function (result) {

            var Sender = "Administrator";
            var Receiver = result.StudentName;
            if (!result.SendToInstructor) {
                Receiver = "Administrator";
                Sender = result.StudentName;
            }

            var message = {
                id: id,
                studentId: result.StudentId,
                studentName: result.StudentName,
                sendtoInstructor: result.SendToInstructor,
                messageSubject: result.MessageSubject,
                messageBody: result.MessageBody,
                studentName: result.StudentName,
                sender: Sender,
                receiver: Receiver
            };

            if (initialBind) {
                ko.applyBindings({ viewModel: message }, document.getElementById("divAcademicAdvisingGet"));
            }

        });
    };

    this.Load = function (id) {
        var academicAdvisingModelObj = new AcademicAdvisingModel();
        academicAdvisingModelObj.Load(id, function (result) {

            var viewModel = {
                id: id,
                studentId: result.StudentId,
                sendToInstructor: result.SendToInstructor,
                messageSubject: ko.observable(result.MessageSubject),
                messageBody: ko.observable(result.MessageBody),

                update: function () {
                    self.Update(this);
                }
            }

            ko.applyBindings(viewModel, document.getElementById("divAcademicAdvisingUpdate"));
        });
    };

    this.Load2 = function (id) {
        var academicAdvisingModelObj = new AcademicAdvisingModel();
        academicAdvisingModelObj.Load(id, function (result) {

            var Sender = "Administrator";
            var Receiver = result.StudentName;
            if (!result.SendToInstructor) {
                Receiver = "Administrator";
                Sender = result.StudentName;
            }

            var viewModel = {
                studentId: result.StudentId,
                studentName: result.StudentName,
                sendToInstructor: !(result.SendToInstructor),
                messageSubject: result.MessageSubject,
                messageBody: result.MessageBody,
                sender : Sender,
                receiver: Receiver,

                respond: function () {
                    self.Create2(this);
                }
            }

            ko.applyBindings(viewModel, document.getElementById("divAcademicAdvisingRespond"));
        });
    };

    this.Create2 = function (data) {

        var model = {
            StudentId: data.studentId,
            StudentName: data.studentName,
            SendToInstructor: data.sendToInstructor,
            MessageSubject: data.messageSubject,
            MessageBody: data.messageBody
        };

        AcademicAdvisingModelObj.Create(model, function (result) {
            if (result == "ok") {
                alert("Create academic advising response successful");
                window.history.go(-2);
            } else {
                alert("Error occurred in adding response");
            }
        });

    };


    this.Update = function (viewModel) {
        var academicAdvisingModelObj = new AcademicAdvisingModel();

        var academicAdvisingData = {
            AcademicAdvisingId: viewModel.id,
            StudentId: viewModel.studentId,
            SendToInstructor: viewModel.sendToInstructor,
            MessageSubject: viewModel.messageSubject,
            MessageBody: viewModel.messageBody
        };

        academicAdvisingModelObj.Update(academicAdvisingData, function (message) {
            $('#divMessage').html(message);
            window.history.go(-2);
        });

    };


    this.Create = function (data) {
        var model = {
            StudentId: data.studentId,
            StudentName: data.studentName,
            SendToInstructor: true,
            MessageSubject: data.messageSubject(),
            MessageBody: data.messageBody()
        }
        AcademicAdvisingModelObj.Create(model, function (result) {
            if (result == "ok") {
                alert("Create academic advising message successful");
            } else {
                alert("Error occurred in adding message");
            }

        });


    };

    this.GetAll = function (studentId, isInstructor) {

        AcademicAdvisingModelObj.GetAll(studentId, isInstructor, function (academicAdvisingList) {
            academicAdvisingListViewModel.removeAll();

            if (academicAdvisingList) {
                for (var i = 0; i < academicAdvisingList.length; i++) {
                    academicAdvisingListViewModel.push({
                        id: academicAdvisingList[i].AcademicAdvisingId,
                        studentId: academicAdvisingList[i].StudentId,
                        studentName: academicAdvisingList[i].StudentName,
                        sendToInstructor: academicAdvisingList[i].SendToInstructor,
                        messageSubject: academicAdvisingList[i].MessageSubject,
                        messageBody: academicAdvisingList[i].MessageBody,
                        senderName: ""
                    });
                }
            }
            if (initialBind) {
                ko.applyBindings({ viewModel: academicAdvisingListViewModel }, document.getElementById("divAcademicAdvisingListContent"));
                initialBind = false;
            }
        });
    };

    this.Delete = function (id) {
        AcademicAdvisingModelObj.Delete(id, function (result) {
            if (result == "ok") {
                alert("Create academic advising message successfully deleted");
            } else {
                alert("Error occurred in deleting message");
            }
        });

    };
}

