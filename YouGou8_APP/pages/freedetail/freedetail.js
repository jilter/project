// pages/freedetail/freedetail.js
var app = getApp()

Page({

  /**
   * 页面的初始数据
   */
  data: {
    indicatorDots: true,
    autoplay: true,
    interval: 2000,
    duration: 500,
    id: 0,
    details: {},
    sdkVersion: app.getSDKVersion(),
    wechat: ''
  },

  previewImg: function (e) {
    wx.previewImage({
      // 当前显示图片的http链接
      current: 'https://63971804.qcloud.la/images/erweima.jpg',
      // 需要预览的图片http链接列表
      urls: ['https://63971804.qcloud.la/images/erweima.jpg']
    })
  },

  inputTyping: function (e) {
    this.setData({
      wechat: e.detail.value
    });
  },

  clearInput: function () {
    this.setData({
      wechat: ""
    })
  },

  getFree: function () {
    var that = this
    if (that.data.wechat == '') {
      wx.showToast({
        title: '请输入微信号',
        icon: 'success',
        duration: 1500
      })
      return
    }
    var newDetails = that.data.details
    var msg = ''
    var userid = app.globalData.memberInfo.id || 0
    if (that.data.sdkVersion >= 110) {
      wx.showLoading({
        title: '加载中',
      })
    }
    wx.request({
      url: app.globalData.apiUrl + '/api/getfree',
      data: {
        freeid: that.data.id,
        userid: userid,
        wechat: that.data.wechat
      },
      header: {
        'content-type': 'application/json'
      },
      success: function (res) {
        if (res.data.code == 0) {
          newDetails.isget = true
          msg = '您已成功申请'
        } else {
          switch (res.data.code) {
            case 1: msg = '活动已结束'; break;
            case 2: msg = '赠品被抢光了'; break;
            case 3: msg = '活动已结束';newDetails.status=false; break;
            case 4: msg = '您已申请过了'; newDetails.isget = true; break;
            case 5: msg = '网络繁忙,稍等5'; break;
            case 6: msg = '网络繁忙,稍等6'; break;
            case 7: msg = '您已领取其它赠品'; newDetails.isgetother = true; break;
            default: msg = '网络出错,稍等'; break;
          }
        }
      },
      fail: function (res) {
        console.log(res)
        msg ='网络出错,稍等'
      },
      complete: function (res) {
        if (msg != '') {
          wx.showToast({
            title: msg,
            icon: 'loading',
            duration: 2000
          })
        }
        that.setData({
          details: newDetails
        });
        console.log(res);
      }
    })
  },

  getDetail: function (id, cb) {
    var that = this
    var userid = app.globalData.memberInfo.id || 0
    if (that.data.sdkVersion >= 110) {
      wx.showLoading({
        title: '加载中',
      })
    }
    wx.request({
      url: app.globalData.apiUrl + '/api/getfreedetail',
      data: {
        freeid: id,
        userid: userid
      },
      method: "post",
      header: {
        'content-type': 'application/json'
      },
      success: function (res) {
        if (that.data.sdkVersion >= 110) {
          wx.hideLoading()
        }
        if (res.data.code == 0) {
          if (res.data.data) {
            that.setData({
              id: id,
              details: res.data.data
            });
          }
        } else {
          wx.showToast({
            title: '活动已结束',
            duration: 2000
          })
          setTimeout(function () {
            wx.hideToast()
            wx.navigateBack()
          }, 2000)
        }
      },
      fail: function (res) {
        console.log(res)
        if (that.data.sdkVersion >= 110) {
          wx.hideLoading()
        }
        wx.navigateBack()
      },
      complete: function (res) {
        typeof cb == "function" && cb()
        console.log(res);
      }
    })
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    wx.setNavigationBarTitle({
      title: '优购8-赠品详细'
    })
    this.getDetail(options.id)
  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {
    var that = this
    that.getDetail(that.data.id, function () { wx.stopPullDownRefresh() })
  }
})