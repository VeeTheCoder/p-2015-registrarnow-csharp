function FinalScheduleModel(asyncIndicator) {
    if (asyncIndicator == undefined) {
        asyncIndicator = true;
    }

    this.GetAll = function (callback) {
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: "http://localhost:9393/api/FinalSchedule/GetFinalSchedule",
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading final schedule list.');
            }
        });
    };

    this.DeleteFinalSchedule = function (id, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/api/FinalSchedule/DeleteFinalSchedule?scheduleId=" + id,
            data: '',
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while final schedule.');
            }
        });
    };

    this.CreateFinalSchedule = function (finalschedule, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/api/FinalSchedule/InsertFinalSchedule",
            data: finalschedule,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while adding final schedule.');
            }
        });
    };

    //update
    this.Load = function (id, callback) {
        $.ajax({     
            async: asyncIndicator,
            method: 'GET',
            url: "http://localhost:9393/api/FinalSchedule/GetCourseFinalSchedule?ScheduleId=" + id,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading final schedule detail.' + id);
            }
        });
    };

    this.UpdateFinalSchedule = function (finalSchedule, callback) {
        $.ajax({
            method: 'POST',
            url: "http://localhost:9393/api/FinalSchedule/UpdateFinalSchedule",
            data: finalSchedule,
            success: function (message) {
                callback(message);
            },
            error: function () {
                callback('Error while updating final schedule info');
            }
        });
    };

    this.Update = function (finalScheduleData, callback) {
        $.ajax({
            method: 'POST',
            url: "http://localhost:9393/api/FinalSchedule/UpdateFinalSchedule",
            data: finalScheduleData, 
            success: function (message) {
                callback(message);
            },
            error: function () {
                callback('Error while updating Final Schedule info');
            }
        });

    };



}

