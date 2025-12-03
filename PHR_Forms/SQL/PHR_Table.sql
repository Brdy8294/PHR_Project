-- -----------------------------------------------------------
-- 1. 측정 항목 정의 테이블 (MEASURE_ITEM)
-- HealthInputForm의 ITEM_CODE(BP01, GL01 등)의 기준이 됩니다.
-- -----------------------------------------------------------
CREATE TABLE MEASURE_ITEM (
    ITEM_CODE VARCHAR2(10) PRIMARY KEY, -- 항목 코드 (예: BP01, GL01)
    ITEM_NAME VARCHAR2(50) NOT NULL,    -- 항목 이름 (예: 수축기 혈압)
    UNIT VARCHAR2(10)                   -- 단위 (예: mmHg, mg/dL)
);

-- 초기 측정 항목 데이터 삽입
INSERT INTO MEASURE_ITEM (ITEM_CODE, ITEM_NAME, UNIT) VALUES ('BP01', '수축기 혈압', 'mmHg');
INSERT INTO MEASURE_ITEM (ITEM_CODE, ITEM_NAME, UNIT) VALUES ('GL01', '혈당', 'mg/dL');
INSERT INTO MEASURE_ITEM (ITEM_CODE, ITEM_NAME, UNIT) VALUES ('WT01', '체중', 'kg');
INSERT INTO MEASURE_ITEM (ITEM_CODE, ITEM_NAME, UNIT) VALUES ('HR01', '심박수', '회/분');

-- -----------------------------------------------------------
-- 2. 측정 로그 시퀀스 (SEQ_MEASURE_LOG)
-- LOG_ID 컬럼의 자동 증가 값을 생성하는 데 사용됩니다.
-- -----------------------------------------------------------
CREATE SEQUENCE SEQ_MEASURE_LOG
START WITH 1
INCREMENT BY 1
NOCACHE
NOCYCLE;

-- -----------------------------------------------------------
-- 3. 측정 기록 테이블 (MEASURE_LOG)
-- HealthInputForm에서 실제로 데이터를 INSERT하는 테이블입니다.
-- -----------------------------------------------------------
CREATE TABLE MEASURE_LOG (
    LOG_ID NUMBER(10) PRIMARY KEY,
    MEMBER_ID NUMBER(10) NOT NULL,
    ITEM_CODE VARCHAR2(10) NOT NULL,
    MEASURE_DATE DATE DEFAULT SYSDATE,
    MEASURE_VALUE NUMBER(10, 2) NOT NULL,
    INPUT_TYPE VARCHAR2(1) DEFAULT 'U'
);

ALTER TABLE MEASURE_LOG ADD CONSTRAINT FK_MEASURE_ITEM FOREIGN KEY (ITEM_CODE) REFERENCES MEASURE_ITEM(ITEM_CODE);

-- 커밋하여 모든 변경사항을 영구적으로 저장
COMMIT;