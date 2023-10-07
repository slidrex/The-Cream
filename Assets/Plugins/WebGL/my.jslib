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