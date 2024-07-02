function FinalScheduleViewModel() {
    var self = this;
    var finalScheduleModelObj = new FinalScheduleModel();
    var initialBind = true;
    var FinalScheduleListViewModel = ko.observableArray();

    //read
    this.GetAll = function () {
        finalScheduleModelObj.GetAll(function (finalScheduleList) {
            FinalScheduleListViewModel.removeAll();

            for (var i = 0; i < finalScheduleList.length; i++) {
                var fnlTimeArr = finalScheduleList[i].FinalTime.split(":");

                FinalScheduleListViewModel.push({
                    id: finalScheduleList[i].Schedule_id,
                    finalLocation: finalScheduleList[i].FinalLocation,
                    finalTime: convertMilitaryTime(fnlTimeArr[0], fnlTimeArr[1], fnlTimeArr[2]),
                    title: finalScheduleList[i].Title + " " + finalScheduleList[i].Schedule_id
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: FinalScheduleListViewModel }, document.getElementById("divFinalScheduleListContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };

    function convertMilitaryTime(hours, minutes, seconds) {
        var standardtime
        var ampm = "AM";
        var d = new Date();
        var mh = parseInt(hours);
        var m = parseInt(minutes);
        var s = parseInt(seconds);
        if (mh > 12) {
            var sh = mh - 12;
            ampm = "PM";
        }
        else {
            sh = mh;
        }
        if (mh < 10) {
            mh = "0" + mh;
        }
        if (m < 10) {
            m = "0" + m;
        }
        if (s < 10) {
            s = "0" + s;
        }

        standardtime = (sh.toString() + ":" +
            m.toString() //+ ":" + s.toString()
            + " " + ampm);

        return standardtime;
    }

    //delete
    ko.bindingHandlers.DeleteFinalSchedule = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var id = viewModel.id;
                finalScheduleModelObj.DeleteFinalSchedule(id, function (result) {
                    if (result != "ok") {
                        alert("Error occurred");
                    } else {
                        FinalScheduleListViewModel.remove(viewModel);
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
            finallocation: ko.observable(""),
            finaltime: ko.observable(""),
            add: function (data) {
                self.CreateFinalSchedule(data);
            }
        };

        ko.applyBindings(viewModel, document.getElementById("divCreateFinalTime"));
    };

    this.CreateFinalSchedule = function (data) {

        var stdTime = (data.finaltime()).replace(" ", ":").split(":");
        var milTime = convertToMilitaryTime(stdTime[2], stdTime[0], stdTime[1]);
        var model = {
            Schedule_id: data.id(),
            Title: data.title(),
            FinalLocation: data.finallocation(),
            FinalTime: milTime
        }

        finalScheduleModelObj.CreateFinalSchedule(model, function (result) {
            if (result == "ok") {
                location.reload();
            } else {
                alert("Error occurred");
            }
        });
    };

    //update
    ko.bindingHandlers.Load = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                if (viewModel) {
                    ko.cleanNode(document.getElementById("divEditFinalSchedule"));
                }

                var id = viewModel.id;

                finalScheduleModelObj.Load(id, function (result) {
                    var fnlTimeArr1 = result.FinalTime.split(":");
                    var fnlTimeConv = convertMilitaryTime(fnlTimeArr1[0], fnlTimeArr1[1], fnlTimeArr1[2]);
                    var viewModel = {
                        id: id,
                        title: result.Title,
                        finallocation: result.FinalLocation,
                        finaltime: fnlTimeConv,
                        update: function () {
                            self.UpdateFinalSchedule(id, this);
                        }
                    }
                        ko.applyBindings(viewModel, document.getElementById("divEditFinalSchedule"));


                    
                });
            });
        }
    }

    this.UpdateFinalSchedule = function (id, viewModel) {
        var stdTime = (viewModel.finaltime).replace(" ", ":").split(":");
        var milTime =  convertToMilitaryTime(stdTime[2], stdTime[0], stdTime[1]);

        var FinalScheduleData = {
            Schedule_id: id,
            Title: viewModel.title,
            FinalLocation: viewModel.finallocation,
            FinalTime: milTime
        };
        finalScheduleModelObj.Update(FinalScheduleData, function (message) {
            $('#divMessage').html(message);
            location.reload();
        });

    };

    function convertToMilitaryTime(ampm, hours, minutes) {
        var militaryHours;
        if (ampm == "AM") {
            militaryHours = hours;
            // check for special case: midnight
            if (militaryHours == "12") { militaryHours = "00"; }
        } else {
            if (ampm == "PM") {
                // get the interger value of hours, then add
                tempHours = parseInt(hours) + 2;
                // adding the numbers as strings converts to strings
                if (tempHours < 10) tempHours = "1" + tempHours;
                else tempHours = "2" + (tempHours - 10);
                // check for special case: noon
                if (tempHours == "24") { tempHours = "12"; }
                militaryHours = tempHours;
            }
        }
        return militaryHours + ":" + minutes + ":00";
    }
}
