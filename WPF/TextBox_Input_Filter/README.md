---
languages:
- csharp
version:
- .NET 6.0
page_type: sample
name: "WPF TextBox input only number (input filter)"
---

# WPF TextBox input only number (input filter)
[Blog Post](https://www.gigong.io/2022/04/17/WPF-TextBox-input-only-number-input-filter)

WPF TextBox의 입력을 제한해보는 프로젝트 입니다.  
다음과 같은 예시가 있습니다.

 - 숫자만 입력
 - 숫자와 - (Hyphen) 입력
 - 문자만 입력 (A ~ Z, a ~ z)
 - G, I, O, N 만 입력 and 방향키 사용 가능 (Can make "GIGONG")

프로그램 실행 후 키 입력시 메인 화면에서 해당 키의 KeyEventArgs.Key 값을 표시해 줍니다.
