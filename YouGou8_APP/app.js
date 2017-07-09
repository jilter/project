//app.js
App({
  onLaunch: function () {
    var that = this
    that.getSysInfo()
    that.getUserInfo(that.getMemberInfo)
  },
  getSysInfo: function (cb) {
    var that = this
    if (that.globalData.sysInfo) {
      typeof cb == "function" && cb(that.globalData.sysInfo)
    } else {
      wx.getSystemInfo({
        success: function (res) {
          that.globalData.sysInfo = res
        }
      })
    }
  },
  canUseCopy: function () {
    var that=this
    console.log('sysInfo', that.globalData.sysInfo)
    if (that.globalData.sysInfo) {
      var sdkVersion = 100
      if (that.globalData.sysInfo.SDKVersion){
        sdkVersion = parseInt(that.globalData.sysInfo.SDKVersion.replace(/\./g, ""))        
      } 
      console.log('sdkVersion',sdkVersion)     
      if (sdkVersion >= 110) {
        return true
      } else {
        return false
      }
    } else {
      return false
    }
  },
  getSDKVersion: function () {
    var that = this
    var sdkVersion = 100
    if (that.globalData.sysInfo) {
      if (that.globalData.sysInfo.SDKVersion) {
        sdkVersion = parseInt(that.globalData.sysInfo.SDKVersion.replace(/\./g, ""))
      }
    }
    return sdkVersion
  },
  getUserInfo: function (cb) {
    var that = this
    if (this.globalData.userInfo) {
      typeof cb == "function" && cb(this.globalData.userInfo)
    } else {
      //调用登录接口
      wx.login({
        success: function (response) {
          console.log('login-success', response)
          wx.getUserInfo({
            success: function (res) {
              console.log('getuserinfo-success', res)
              that.globalData.userInfo = res.userInfo
              typeof cb == "function" && cb(that.globalData.userInfo)
            },
            fail: function (res) {
              wx.getStorage({
                key: 'memberInfo',
                success: function (res) {
                  console.log('getStorage-success', res.data)
                  if (res.data) {
                    that.globalData.memberInfo = res.data
                  } else {
                    var d = new Date();
                    typeof cb == "function" && cb({ NickName: response.code, AvatarUrl: d.getTime() })
                  }
                },
                fail: function (res) {
                  var d = new Date();
                  typeof cb == "function" && cb({ NickName: response.code, AvatarUrl: d.getTime() })
                  console.log('getStorage-fail', res)
                }
              })
              console.log('getuserinfo-fail', res)
            }
          })
        },
        fail: function (response) {
          wx.getStorage({
            key: 'memberInfo',
            success: function (res) {
              if (res.data) {
                that.globalData.memberInfo = res.data
              } else {
                var d = new Date();
                typeof cb == "function" && cb({ NickName: d.getTime(), AvatarUrl: d.getTime() })
              }
            }
          })
          console.log('login-fail', response)
        }
      })
    }
  },
  getMemberInfo: function (data) {
    var that = this
    wx.request({
      url: that.globalData.apiUrl + '/api/getmemberinfo',
      data: data,
      header: {
        'content-type': 'application/json'
      },
      success: function (res) {
        if (res.data.code == 0) {
          that.globalData.memberInfo = res.data.data
          wx.setStorage({ key: 'memberInfo', data: that.globalData.memberInfo })
          console.log('memberInfo', that.globalData.memberInfo)
        }
      },
      fail: function (res) {
        console.log('getmemberinfo-fail', res);
      }
    })
  },
  globalData: {
    apiUrl: 'https://63971804.qcloud.la',
    memberInfo: null,
    userInfo: null,
    sysInfo: null
  }
})