var myPlugin = {

    ShowAdv : function(){
            ysdk.adv.showFullscreenAdv({
              callbacks: {
              onClose: function(wasShown) {
                
                // some action after close
            	},
              onError: function(error) {
                // some action on error
            	}
            }
            })
        },
    SaveExtern: function(date){
        var dateString = UTF8ToString(date);
        var myObj = JSON.parse(dateString);
        player.setData(myObj);
    },
    LoadExtern: function(){
        player.getData().then(_date => {
            const myJSON = JSON.stringify(_date);
            myGameInstance.SendMessage('Yandex', 'LoadData', myJSON);
        });
    },

  	RateGame: function () {
    
    	ysdk.feedback.canReview()
        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.log(feedbackSent);
                    })
            } else {
                console.log(reason)
            }
        })
  	}
};

mergeInto(LibraryManager.library, myPlugin);