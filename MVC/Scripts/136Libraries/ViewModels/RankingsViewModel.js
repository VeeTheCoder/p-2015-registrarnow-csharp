function RankingsViewModel() {
    var initialLoad = false;

    // manage rankings index
    this.Load = function () {
        var scheduleModelObj = new ScheduleModel();

        // Because the Load() is a async call (asynchronous), we'll need to use
        // the callback approach to handle the data after data is loaded.
        scheduleModelObj.Load2(function (scheduleListData) {

            // courseList - presentation layer model retrieved from /Course/GetCourseList route.
            // courseListViewModel - view model for the html content
            var scheduleListViewModel = new Array();

            // DTO from the JSON model to the view model. In this case, courseListViewModel doesn't need the "id" attribute
            for (var i = 0; i < scheduleListData.length; i++) {
                scheduleListViewModel[i] = {
                    scheduleid: scheduleListData[i].ScheduleId,
                    year: scheduleListData[i].Year,
                    session: scheduleListData[i].Session,
                    quarter: scheduleListData[i].Quarter,
                    title: scheduleListData[i].Course.Title,
                    description: scheduleListData[i].Course.Description
                };
            }

            // this is using knockoutjs to bind the viewModel and the view (Home/Index.cshtml)
            ko.applyBindings({ viewModel: scheduleListViewModel }, document.getElementById("divRankingsListContent"));
        });
    };

    var clickHandle = function (scheduleid, studentid, rank) {
        if (initialLoad) {
            var rankingsModelObj = new RankingsModel();

            rankingsModelObj.Insert(scheduleid, studentid, rank, function () {
                location.reload();
            });
        }
        initialLoad = true;
    };

    // rankings for a schedule
    this.Load2 = function (id) {
        var rankingsModelObj = new RankingsModel();

        // Because the Load() is a async call (asynchronous), we'll need to use
        // the callback approach to handle the data after data is loaded.
        rankingsModelObj.Load(id, function (rankingListData) {

            var rankingViewModel = {
                data: new Array(),
                studentids: ko.observableArray(),
                handleClick: clickHandle,
                studentidField: ko.observable(),
                rankidField: ko.observable()
           }

            if (rankingListData) {
                // DTO from the JSON model to the view model. In this case, courseListViewModel doesn't need the "id" attribute
                for (var i = 0; i < rankingListData.length; i++) {
                    rankingViewModel.data[i] = {
                        scheduleid: rankingListData[i].ScheduleId,
                        studentid: rankingListData[i].StudentId,
                        rank: rankingListData[i].Rank,
                        started: false,
                        handle: function () {
                            if (this.started) {
                                rankingsModelObj.Delete(this.studentid, this.scheduleid, function () { });
                                location.reload();
                            }
                            this.started = true;
                        }
                    };
                }
            }

            var enrollmentModelObj = new EnrollmentModel();

            enrollmentModelObj.Get(id, function (studentListData) {
                if (studentListData) {
                    // DTO from the JSON model to the view model. In this case, courseListViewModel doesn't need the "id" attribute
                    for (var i = 0; i < studentListData.length; i++) {
                        if (!studentListData[i].Rank) {
                            rankingViewModel.studentids.push(studentListData[i].StudentId);
                        }
                    }
                }
                ko.applyBindings({ viewModel: rankingViewModel }, document.getElementById("divRankingsContent"));
            });
           });
    };
}