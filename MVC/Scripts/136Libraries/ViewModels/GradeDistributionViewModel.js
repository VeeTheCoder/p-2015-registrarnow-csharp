function GradeDistributionViewModel() {
    var self = this;
    var gradeDistributionModelObj = new GradeDistributionModel();
    var initialBind = true;
    var GradeDistributionListViewModel = ko.observableArray();

    function isNumber(obj) { return !isNaN(parseFloat(obj)) }

    //read
    this.GetAll = function () {
        gradeDistributionModelObj.GetAll(function (gradeDistributionList) {
            GradeDistributionListViewModel.removeAll();
            for (var i = 0; i < gradeDistributionList.length; i++) {

                var grdDistArr = gradeDistributionList[i].Grade_Distribution.split(",");
                var gradeA = grdDistArr[0] + "%";
                var gradeB = grdDistArr[1] + "%";
                var gradeC = grdDistArr[2] +  "%";
                var gradeD = grdDistArr[3] +  "%";
                var gradeF = grdDistArr[4] +  "%";
                GradeDistributionListViewModel.push({
                    title: gradeDistributionList[i].Title + " " + gradeDistributionList[i].Schedule_id,
                    grdA: gradeA, grdB: gradeB, grdC: gradeC, grdD: gradeD, grdF: gradeF,
                    id:gradeDistributionList[i].Schedule_id
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: GradeDistributionListViewModel }, document.getElementById("divGradeDistributionListContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };


    //delete
    ko.bindingHandlers.DeleteGradeDistribution = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var id = viewModel.id;
                gradeDistributionModelObj.DeleteGradeDistribution(id, function (result) {
                    if (result != "ok") {
                        alert("Error occurred");
                    } else {
                        GradeDistributionListViewModel.remove(viewModel);
                        location.reload();
                    }
                });
            });
        }
    }

    //create
    this.Initialize = function () {
        var viewModel = {
            id: ko.observable(""),
            title: ko.observable(""),
            gradedistribution: ko.observable(""),
            add: function (data) {
                self.CreateGradeDistribution(data);
            }
        };

        ko.applyBindings(viewModel, document.getElementById("divCreateGradeDistribution"));
    };

    this.CreateGradeDistribution = function (data) {
        var model = {
            Schedule_id: data.id(),
            Title: data.title(),
            Grade_Distribution: data.gradedistribution()
        }

        var grdDistArr = model.Grade_Distribution.split(",");

        if(grdDistArr.length == 5)
        {
            if (isNumber(grdDistArr[0]) && isNumber(grdDistArr[1]) && isNumber(grdDistArr[2]) && isNumber(grdDistArr[3])
                && isNumber(grdDistArr[4])) {
                gradeDistributionModelObj.CreateGradeDistribution(model, function (result) {
                    if (result == "ok") {
                        location.reload();
                    } else {
                        alert("Error occurred");
                    }
                });
            }
         }
    };

    //update
    ko.bindingHandlers.Load = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {

                if (viewModel) {
                    ko.cleanNode(document.getElementById("divEditGradeDistribution"));
                }

                var id = viewModel.id;

                gradeDistributionModelObj.Load(id, function (result) {
                    var viewModel = {
                        id: id,
                        title: result.Title,
                        gradedistribution: result.Grade_Distribution,
                        update: function () {
                            self.UpdateGradeDistribution(id, this);
                        }
                    }
                    ko.applyBindings(viewModel, document.getElementById("divEditGradeDistribution"));

                });
            });
        }
    }

    this.UpdateGradeDistribution = function (id, viewModel) {
        var GradeDistributionData = {
            Schedule_id: id,
            Title: viewModel.title,
            Grade_Distribution: viewModel.gradedistribution
        };

        var grdDistArr = GradeDistributionData.Grade_Distribution.split(",");

        if (grdDistArr.length == 5) {
            if (isNumber(grdDistArr[0]) && isNumber(grdDistArr[1]) && isNumber(grdDistArr[2]) && isNumber(grdDistArr[3])
                && isNumber(grdDistArr[4])) {
                gradeDistributionModelObj.Update(GradeDistributionData, function (message) {
                    $('#divMessage').html(message);
                    location.reload();
                });
            }
        }

    };


}





