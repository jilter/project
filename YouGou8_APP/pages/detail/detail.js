// pages/detail/detail.js
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
    canUseCopy: false,
    sdkVersion: app.getSDKVersion()
  },

  copy: function (e) {
    var that = this
    wx.setClipboardData({
      data: "复制这条信息" + that.data.details.key + ",打开“手.机.淘.宝”即可查看并下单!",
      success: function (res) {
        wx.showToast({
          title: '复制成功',
          icon: 'success',
          duration: 2000
        })
      }
    })
  },

  showAction: function (e) {
    wx.showActionSheet({
      itemList: ['保存图片'],
      success: function (res) {
        wx.downloadFile({
          url: 'https://63971804.qcloud.la/images/erweima.jpg',
          success: function (res) {
            wx.saveFile({
              tempFilePath: res.tempFilePath,
              success: function (res) {
                console.log(res)
              }
            })
          }
        })
      },
      fail: function (res) {
        console.log(res.errMsg)
      }
    })
  },

  previewImg: function (e) {
    wx.previewImage({
      // 当前显示图片的http链接
      current: 'https://63971804.qcloud.la/images/erweima.jpg',
      // 需要预览的图片http链接列表
      urls: ['https://63971804.qcloud.la/images/erweima.jpg']
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
      url: app.globalData.apiUrl + '/api/getdetail',
      data: {
        id: id,
        userid: userid
      },
      method: "post",
      header: {
        'content-type': 'application/json'
      },
      success: function (res) {
        if (res.data.code == 0) {
          if (res.data.data) {
            that.setData({
              id: id,
              details: res.data.data
            });
          }
          if (that.data.sdkVersion >= 110) {
            wx.hideLoading()
          }
        } else {
          wx.showToast({
            title: '产品已下架',
            icon: 'loading',
            duration: 2000
          })
          setTimeout(function () {
            wx.navigateBack()
          }, 2000)
        }
      },
      fail: function (res) {
        console.log(res)
        wx.showToast({
          title: '网络连接失败',
          icon: 'loading',
          duration: 2000
        })
        setTimeout(function () {
          wx.navigateBack()
        }, 2000)
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
    this.setData({
      canUseCopy: app.canUseCopy()
    })
    wx.setNavigationBarTitle({
      title: '优购8-详情页'
    })
    this.getDetail(options.id)
  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {
    var that = this
    that.setData({
      canUseCopy: app.canUseCopy()
    })
    that.getDetail(that.data.id, function () { wx.stopPullDownRefresh() })
  }
})