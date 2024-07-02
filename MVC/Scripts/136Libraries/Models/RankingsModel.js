function RankingsModel() {

    this.Load = function (scheduleid, callback) {
        var url = "http://localhost:9393/api/Ranking/GetRankingList?scheduleId=" + scheduleid;
        $.ajax({
            url: url,
            data: "",
            dataType: "json",
            success: function (rankingListData) {
                callback(rankingListData);
            },
            error: function () {
                alert('Error while loading rank list.  Is your service layer running?');
            }
        });
    };

    this.Get = function (scheduleid, studentid, callback) {
        var url = "http://localhost:9393/api/Ranking/GetRankingInfo?studentId="
            + studentid + "&scheduleId=" + scheduleid;
        $.ajax({
            method: "GET",
            url: url,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while getting rank.  Is your service layer running?');
            }
        });

    };

    this.Delete = function (studentid, scheduleid, callback) {
        $.ajax({
            method: "POST",
            url: "http://localhost:9393/Api/Ranking/DeleteRanking?studentId=" + studentid
                + "&scheduleId=" + scheduleid,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while deleteing ranking.  Is your service layer running?');
            }
        });
    };

    this.Insert = function (scheduleid, studentid, rank, callback) {
        var data = {
            StudentId: studentid,
            ScheduleId: scheduleid,
            Rank: parseInt(rank)
        };

        var url = "http://localhost:9393/Api/Ranking/InsertRanking";
        $.ajax({
            method: "POST",
            url: url,
            data: data,
            dataType: "json",
            success: function (result) {
                if (result == "ok") {
                    alert("Create ranking successful");
                    callback();
                } else {
                    alert("Error occurred");
                }
            },
            error: function () {
                alert('Error inserting.');
            }
        });
    };
}
