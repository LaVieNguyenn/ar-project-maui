# TryOnAR MAUI + MediaPipe Integration

## 🎯 Mục tiêu dự án

> **NOTE QUAN TRỌNG**  
> **Repository này _KHÔNG_ bao gồm thư viện `mediaPipe_android.aar` + `unityLibrary.aar` (MediaPipe Unity Android Plugin) do giới hạn dung lượng GitHub.**  
> Bạn **phải tự tải** file `.aar` này từ [repo chính thức MediaPipeUnity](https://github.com/homuler/MediaPipeUnityPlugin/releases) hoặc **liên hệ tác giả** để nhận file `.aar` dùng cho Android.  

> Bạn **phải tự vô Unity project đính kèm và export** file `unityLibrary.aar` để sử dụng hoặc **liên hệ tác giả** để nhận file `.aar` dùng cho Android.  

> Sau đó hãy copy vào thư mục `Assets/MediaPipeUnity/Plugins/Android/` hoặc vị trí yêu cầu trong project để sử dụng.


**TryOnAR MAUI** là dự án ứng dụng thử phụ kiện (kính, mũ, v.v.) thời gian thực sử dụng công nghệ **AR (Augmented Reality)**, phát triển bằng .NET MAUI và tích hợp MediaPipe (Google) để nhận diện khuôn mặt và ước lượng pose 3D.  
Mục tiêu:  
- Mang lại trải nghiệm thử phụ kiện “ảo” trực tiếp trên nhiều nền tảng (Android, iOS, Windows, Mac).
- Dễ dàng mở rộng, tùy biến UI với công nghệ .NET MAUI hiện đại, tận dụng Unity cho rendering 3D mạnh mẽ.

---

## 🏗️ Kiến trúc hệ thống


- **Unity Scene**: 
    - Camera, RawImage (hiển thị video), phụ kiện AR (Prefab).
    - MediaPipe FaceLandmarkRunner.
    - HeadAccessoryManager (gắn, scale, auto-fit phụ kiện).
- **MediaPipeUnity**:
    - Thư viện landmark detection real-time.
    - Plugins cho từng nền tảng: Windows (.dll), Android (.aar), iOS (.framework) (chỉ dùng Android/Windows ở dự án này).
- **.NET MAUI**:
    - App shell/native cho hiển thị Unity, gọi xử lý Unity scene thông qua bridge/native plugin (hỗ trợ truyền frame, nhận event).


---

## ⚡️ Tính năng chính

- Nhận diện khuôn mặt nhanh/chính xác, hỗ trợ nhiều góc mặt (nghiêng/trái/phải/lên/xuống).
- Thử phụ kiện 3D theo thời gian thực (auto-fit vị trí/pose).
- Điều chỉnh, custom phụ kiện trực tiếp (UI tuning).
- Đa nền tảng: Android, iOS, Windows, MacOS (tùy cấu hình backend MediaPipe).

---

## 🚀 Hướng dẫn cài đặt & chạy thử

### 1. Cài đặt môi trường

- **.NET 8+, MAUI** (Visual Studio 2022+ hoặc JetBrains Rider).
- **Unity 2022.3+** (Hỗ trợ build cho Android/iOS).
- **MediaPipe Unity Plugin** (ví dụ [homuler/MediaPipeUnityPlugin](https://github.com/homuler/MediaPipeUnityPlugin)), hoặc MediaPipe Tasks.

### 2. Tích hợp Unity vào MAUI

- Sử dụng [UnityUaal.Maui](https://github.com/matthewrdev/UnityUaal.Maui) hoặc MAUI-Unity binding (UAAL/Unity as a Library) theo hướng dẫn repo chính.
- Build Unity project thành `.aar` (Android) hoặc framework (iOS), tích hợp vào dự án MAUI.

### 3. Kết nối MediaPipe vào Unity

- Import MediaPipe Unity package.
- Tùy từng backend, config pipeline nhận diện face landmark, expose pose matrix & landmark cho script phụ kiện.

### 4. Cấu hình AR TryOn

- Attach các script như `HeadAccessoryManager` (tham khảo trong repo), cấu hình index landmark, scale/offset.
- Gán các Prefab 3D phụ kiện (glasses, hat, etc).
- Build & thử nghiệm trên device thật.

---

## 🧑‍💻 Custom/Tuning

- Có thể bổ sung UI bằng XAML (MAUI) để chọn model phụ kiện, scale, màu sắc.
- Có thể điều chỉnh trực tiếp offset/scale phụ kiện trong Unity Editor.
- Debug log chi tiết được bật sẵn trong các script để tune nhanh khi lên device.

---

## ⚠️ Limitation & Ghi chú

### **1. MediaPipe orientation mismatch**
- MediaPipe trả về landmark/pose matrix luôn theo **landscape**, dù raw camera image (RawImage) đang portrait.
- Cần xử lý xoay lại toạ độ (rotate landmark/matrix) để mapping đúng với RawImage và phụ kiện (nhất là trên mobile).

### **2. Chênh lệch scale/offset**
- Tùy theo aspect ratio, orientation (portrait/landscape), device size... có thể phải **auto-scale lại** phụ kiện.  
- Vấn đề “phụ kiện bị xa mặt” hoặc “không bám sát mặt” là do scale Z, offset chưa khớp.
- Để khớp tuyệt đối với user, cần tune kỹ scale, offset và landmark reference.

### **3. Độ trễ & hiệu năng**
- MediaPipe chạy real-time nhưng vẫn có độ trễ nhỏ, khi xoay mặt quá nhanh có thể phụ kiện trễ theo.
- Unity build cần tối ưu cho mobile (chọn shader, culling...), tránh lag/fps drop khi chạy nhiều phụ kiện hoặc multi-face.

### **4. Tích hợp Unity + MAUI còn experimental**
- Một số chức năng không hỗ trợ hết trên tất cả device/OS.
- Một số API của Unity chưa expose toàn bộ cho MAUI, cần kiểm tra version kỹ lưỡng.

### **5. Camera permission & raw image**
- Việc lấy frame camera và truyền đúng cho MediaPipe là quan trọng.
- Camera phải trả về đúng orientation, đúng aspect ratio theo device.

### **6. Hạn chế MediaPipe Tasks**
- Một số backend chỉ support Android/iOS (không chạy native trên Windows/Mac, chỉ hỗ trợ simulate).
- Muốn chạy trên desktop cần MediaPipe C++ hoặc Python backend.

### **7. Độ chính xác landmark/pose**
- Phụ thuộc model MediaPipe, ánh sáng môi trường, tốc độ xoay mặt.  
- Cần xử lý fallback nếu không detect được mặt hoặc landmark trả về thiếu.

---

## 💡 Hướng phát triển

- Hỗ trợ multi-face, multi-accessory.
- Streaming phụ kiện AR sang backend server.
- Phát triển thêm UX cho việc chọn phụ kiện, tùy chỉnh, selfie v.v.
- Tối ưu hóa hiệu năng cho thiết bị tầm trung, low-end.

---

## 📄 Tham khảo

- [MediaPipe Unity Plugin](https://github.com/homuler/MediaPipeUnityPlugin)
- [Unity as a Library (UAAL)](https://docs.unity3d.com/Manual/UnityasaLibrary.html)
- [UnityUaal.Maui](https://github.com/matthewrdev/UnityUaal.Maui)
- [MediaPipe Tasks](https://developers.google.com/mediapipe/solutions/vision/face_landmarker)

---

**Mọi thắc mắc/tư vấn thêm vui lòng tạo issue hoặc liên hệ nhóm phát triển!**

---

## 🤝 Góp ý & Đóng góp

Đây là một **dự án self-project** (dự án cá nhân, thử nghiệm công nghệ).  
Mình **khuyến khích mọi người fork repo về để tuỳ biến, update và phát triển thêm** tính năng hoặc tích hợp các pipeline MediaPipe mới, cải tiến auto-fit, tối ưu UI, v.v.  
- Nếu bạn có ý tưởng hoặc giải pháp tối ưu scale, bám mặt, mapping chuẩn hơn cho phụ kiện 3D, hãy tạo pull request hoặc issue để cùng thảo luận!
- Các bản cập nhật/commit về xử lý orientation, đa nền tảng, hoặc nâng cấp MediaPipe, Unity, MAUI... đều được hoan nghênh.

**Let's build better AR Try-On together!**

---
