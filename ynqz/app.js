//app.js
App({
  onLaunch: function() {
    //调用API从本地缓存中获取数据
    var that = this;
    wx.login({
      success: function (res) {
        console.log(res)
        wx.request({
          //获取openid接口
          url: 'https://api.weixin.qq.com/sns/jscode2session',
          data: {
            appid: that.globalData.appID,
            secret: that.globalData.appSecret,
            js_code: res.code,
            grant_type: 'authorization_code'
          },
          method: 'GET',
          success: function (res) {
            console.log(res)
            that.globalData.openId = res.data.openid;
            that.globalData.sessionKey = res.data.session_key;
          }
        })
      }
    })
  },

  getUserInfo: function(cb) {
    var that = this
    if (that.globalData.userInfo) {
      typeof cb == "function" && cb(this.globalData.userInfo)
    } else {
      //调用登录接口
      wx.getUserInfo({
        withCredentials: false,
        success: function(res) {
          that.globalData.userInfo = res.userInfo
          typeof cb == "function" && cb(that.globalData.userInfo)
        }
      })
    }
  },

  globalData: {
    userInfo: null,
    openId:null,
    sessionKey:null,
    apiUrl:"https://63971804.qcloud.la",
    appID:"wx26765864ab840c45",
    appSecret:"4809032813e2a1e0c6f09fb7bc6ea255"
  }
})
