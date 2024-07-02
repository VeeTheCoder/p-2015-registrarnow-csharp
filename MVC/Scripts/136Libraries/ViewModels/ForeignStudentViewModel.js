function ForeignStudentViewModel() {

    var ForeignStudentModelObj = new ForeignStudentModel();
    var self = this;
    var initialBind = true;
    var foreignStudentListViewModel = ko.observableArray();
    var changeable = ko.observable(false);

    this.Initialize = function (id) {

        var viewModel = {
            sid: ko.observable("A000009"),
            Under_gpa: ko.observable("3.56"),
            toefl: ko.observable("730"),
            editable: ko.observable(false),
            deletabe: ko.observable(false),
            add: function (data) {
                self.CreateForeignStudent(data);
            },
            changing: function (data) {
                data.viewModel.editable(true);
            },
            Update: UpdateForeignStudent,
            Cancel: function (data) {
                data.viewModel.editable(false);
            }

        };

        ForeignStudentModelObj.GetDetail(id, function (result) {
            if (viewModel != null) {
                viewModel.sid(result.Sid);
                viewModel.Under_gpa(result.Under_gpa);
                viewModel.toefl(result.Toefl);
            }

        });

        ko.applyBindings({ viewModel: viewModel }, document.getElementById("divForeignStudent"));
    };

    var UpdateForeignStudent = function (data) {
        var foreignModelObj = new ForeignStudentModel();

        // convert the viewModel to same structure as PLAdmin model (presentation layer model)
        var foreignData = {
            sid: data.viewModel.sid(),
            Under_gpa: data.viewModel.Under_gpa(),
            toefl: data.viewModel.toefl()
        };

        foreignModelObj.Update(foreignData, function (result) {
            data.viewModel.editable(false);
        });

    };

    /*
    var DeleteForeignStudent = function (data) {
        //var gradModelObj = new GradStudentModel();

        foreignModelObj.Delete(id, function (result) {
            data.viewModel.editable(false);
            if (result == "ok") {
                initialize(id);
            }
        });

    };
*/



    this.createForeignStudent = function (data) {
        var model = {
            sid: data.sid(),
            Under_gpa: data.Under_gpa(),
            Toefl: data.Toefl()
        }

        ForeignStudentModelObj.Create(model, function (result) {
            if (result == "ok") {
                alert("Create foreign student successful");
            } else {
                alert("Error occurred");
            }
        });

    };


    this.initializeForeignStudent = function () {

        var viewModel2 = {
            sid: ko.observable("A000003"),
            Under_gpa: ko.observable("3.52"),
            Toefl: ko.observable("752.03"),
            create: function (data) {
                self.createForeignStudent(data);
            }
        };

        ko.applyBindings(viewModel2, document.getElementById("divForeignStudent"));
    };


    this.GetAll = function () {
        ForeignStudentModelObj.GetAll(function (foreignStudentList) {
            foreignStudentListViewModel.removeAll();
            for (var i = 0; i < foreignStudentList.length; i++) {
                foreignStudentListViewModel.push({
                    sid: foreignStudentList[i].Sid,
                    under_gpa: foreignStudentList[i].Under_gpa,
                    toefl: foreignStudentList[i].Toefl
                });

            }

            if (initialBind) {
                ko.applyBindings({ viewModel: foreignStudentListViewModel }, document.getElementById("divForeignStudentListContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };



    this.GetDetail = function (id) {

        ForeignStudentModelObj.GetDetail(id, function (result) {

            var foreignStudent = {
                sid: result.Sid,
                Under_gpa: result.Under_gpa,
                Toefl: result.Toefl
            };

            if (initialBind) {
                ko.applyBindings({ viewModel: foreignStudent }, document.getElementById("divForeignStudentContent"));
            }
        });
    };

    ko.bindingHandlers.DeleteForeignStudent = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function (data) {
                var id = viewModel.sid;

                ForeignStudentModelObj.Delete(id, function (result) {
                    if (result != "ok") {
                        alert("Error occurred");
                    } else {
                        foreignStudentListViewModel.remove(viewModel);
                    }
                });
            });
        }
    }
}
