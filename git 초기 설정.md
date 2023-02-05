# git 초기 설정

1. 새로운 Repository 만들기
2. [자료.홈페이지.한국](http://자료.홈페이지.한국)에 접속하여 .gitignore, .gitattribute 다운로드
3. 다운받은 .gitignore를 push하기
4. Repository 폴더에 들어가서 bash를 열고 아래 명령어를 작성

```jsx
git lfs install
git lfs track ".jpg"
```

1. 위 명령어 작성시 .gitattribute 파일이 생성되어 있음.
2. [자료.홈페이지.한국](http://자료.홈페이지.한국)에서 다운받은 .gitattribute 파일로 대체
3. 변경사항을 다시 push하기
4. 이후 큰 파일을 github desktop으로 업로드가 가능하다.

하지만 현원이는 4번 과정에서 bash를 사용할 때, 오류가 발생한다. 다른 컴퓨터를 통해 이 과정을 수행해주면 가능하다.