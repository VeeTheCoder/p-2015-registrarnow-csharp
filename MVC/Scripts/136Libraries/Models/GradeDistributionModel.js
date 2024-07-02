function GradeDistributionModel(asyncIndicator) {
    if (asyncIndicator == undefined) {
        asyncIndicator = true;
    }

    this.GetAll = function (callback) {
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: "http://localhost:9393/api/GradeDistribution/GetGradeDistribution",
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading grade distribution list.');
            }
        });
    };

    this.DeleteGradeDistribution = function (id, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/api/GradeDistribution/DeleteGradeDistribution?scheduleId=" + id,
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

    this.CreateGradeDistribution = function (finalschedule, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/api/GradeDistribution/InsertGradeDistribution",
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
            url: "http://localhost:9393/api/GradeDistribution/GetCourseGradeDistribution?ScheduleId=" + id,
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

    this.Update = function (gradeDistributionData, callback) {
        $.ajax({
            method: 'POST',
            url: "http://localhost:9393/api/GradeDistribution/UpdateGradeDistribution",
            data: gradeDistributionData,
            success: function (message) {
                callback(message);
            },
            error: function () {
                callback('Error while updating Final Schedule info');
            }
        });
    };
}
