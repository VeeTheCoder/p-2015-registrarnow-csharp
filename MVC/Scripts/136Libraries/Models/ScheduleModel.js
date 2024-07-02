function ScheduleModel(asyncIndicator) {
    if (asyncIndicator == undefined) {
        asyncIndicator = true;
    }

    this.Create = function (schedule, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Schedule/InsertSchedule",
            data: schedule,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while adding schedule.  Is your service layer running?');
            }
        });
    };

    this.Load = function (year, quarter, callback) {
        $.ajax({
            url: "http://localhost:9393/Api/Schedule/GetScheduleList?year=" + year +
                "&quarter=" + quarter,
            data: "",
            dataType: "json",
            success: function (scheduleListData) {
               callback(scheduleListData);
            },
            error: function () {
                alert('Error while loading schedule list.  Is your service layer running?');
            }
        });
    };

    this.Load2 = function (callback) {
        $.ajax({
            url: "http://localhost:9393/Api/Schedule/GetScheduleList",
            data: "",
            dataType: "json",
            success: function (scheduleListData) {
                callback(scheduleListData);
            },
            error: function () {
                alert('Error while loading schedule list.  Is your service layer running?');
            }
        });
    };
}
