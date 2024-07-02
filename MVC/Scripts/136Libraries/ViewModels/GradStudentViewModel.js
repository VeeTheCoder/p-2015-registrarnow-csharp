function GradStudentViewModel() {

    var GradStudentModelObj = new GradStudentModel();
    var self = this;
    var initialBind = true;
    var gradStudentListViewModel = ko.observableArray();
    var changeable = ko.observable(false);

    this.Initialize = function (id) {

        var viewModel = {
            sid: ko.observable("A000009"),
            Under_gpa: ko.observable("3.56"),
            gre: ko.observable("730"),
            editable: ko.observable(false),
            deletabe: ko.observable(false),
            add: function (data) {
                self.CreateGradStudent(data);
            },
            changing: function (data) {
                data.viewModel.editable(true);
            },
            Update: UpdateGradStudent,
            Cancel: function (data) {
                data.viewModel.editable(false);
            }

        };

        GradStudentModelObj.GetDetail(id, function (result) {
            if (viewModel != null) {
                viewModel.sid(result.Sid);
                viewModel.Under_gpa(result.Under_gpa);
                viewModel.gre(result.Gre);
            }

        });

        ko.applyBindings({ viewModel: viewModel }, document.getElementById("divGradStudent"));
    };

    var UpdateGradStudent = function (data) {
        var gradModelObj = new GradStudentModel();

        // convert the viewModel to same structure as PLAdmin model (presentation layer model)
        var gradData = {
            sid: data.viewModel.sid(),
            Under_gpa: data.viewModel.Under_gpa(),
            gre: data.viewModel.gre()
        };

        gradModelObj.Update(gradData, function (result) {
            data.viewModel.editable(false);
        });

    };

    var DeleteGradStudent = function (data) {
        //var gradModelObj = new GradStudentModel();

        gradModelObj.Delete(id, function (result) {
            data.viewModel.editable(false);
            if (result == "ok") {
                initialize(id);
            }
        });

    };




    this.createGradStudent = function (data) {
        var model = {
            sid: data.sid(),
            Under_gpa: data.Under_gpa(),
            Gre: data.Gre()
        }

        GradStudentModelObj.Create(model, function (result) {
            if (result == "ok") {
                alert("Create grad student successful");
            } else {
                alert("Error occurred");
            }
        });

    };


    this.initializeGradStudent = function () {

        var viewModel2 = {
            sid: ko.observable("A000009"),
            Under_gpa: ko.observable("3.56"),
            Gre: ko.observable("730"),
            create: function (data) {
                self.createGradStudent(data);
            }
        };

        ko.applyBindings(viewModel2, document.getElementById("divGradStudent"));
    };


    this.GetAll = function () {

        GradStudentModelObj.GetAll(function (gradStudentList) {
            gradStudentListViewModel.removeAll();

            for (var i = 0; i < gradStudentList.length; i++) {
                console.log(i);
                gradStudentListViewModel.push({
                    sid: gradStudentList[i].Sid,
                    under_gpa: gradStudentList[i].Under_gpa,
                    gre: gradStudentList[i].Gre
                });
                alert(i + " " + gradStudentList[i].Under_gpa);

            }

            if (initialBind) {
                ko.applyBindings({ viewModel: gradStudentListViewModel }, document.getElementById("divGradStudentListContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };



    this.GetDetail = function (id) {

        GradStudentModelObj.GetDetail(id, function (result) {

            var gradStudent = {
                sid: result.Sid,
                Under_gpa: result.Under_gpa,
                Gre: result.Gre
            };

            if (initialBind) {
                ko.applyBindings({ viewModel: gradStudent }, document.getElementById("divGradStudentContent"));
            }
        });
    };

    ko.bindingHandlers.DeleteGradStudent = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function (data) {
                var id = viewModel.sid;

                GradStudentModelObj.Delete(id, function (result) {
                    if (result != "ok") {
                        alert("Error occurred");
                    } else {
                        gradStudentListViewModel.remove(viewModel);
                    }
                });
            });
        }
    }
}