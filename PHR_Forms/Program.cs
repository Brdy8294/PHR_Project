using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using PHR_Management_App; // DBClass와 HospitalForm에서 Oracle 관련 클래스를 사용하므로 추가

// PHR_Forms 네임스페이스를 PHR_Project로 변경하거나, HospitalForm의 네임스페이스에 맞추는 것이 좋습니다.
// 현재는 DBClass가 PHR_Project 네임스페이스에 있으므로, Program도 이 네임스페이스에 맞춥니다.
namespace PHR_Project
{
    // 이전 대화에서 사용자가 제공한 DBClass와 HospitalForm이 이 네임스페이스에 있다고 가정합니다.

    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. 데이터베이스 연결 관리 클래스(DBClass) 객체 생성
            DBClass db = new DBClass();
            db.DB_ObjCreate();

            // 2. HealthInputForm을 주 폼으로 설정하고 DBClass 객체를 인자로 전달
            // 참고: DB 연결은 Form의 Load 이벤트에서 처리하는 것이 디자이너 오류를 방지하는 좋은 방법입니다.
            Application.Run(new HealthInputForm(db));
        }
    }

    // 이전에 제공된 DBClass의 정의가 여기에 포함되어 있거나 (하나의 파일에), 
    // 혹은 별도의 DBClass.cs 파일에 올바르게 정의되어 있어야 합니다.
    // 만약 DBClass가 HospitalForm과 Program.cs에서만 사용된다면 여기에 DBClass 전체 코드를 붙여넣어도 무방합니다.
    // 여기서는 DBClass가 이미 별도의 파일에 있다고 가정하고 Program.cs만 수정했습니다.

    // 주의: HospitalForm 클래스는 이 파일에 없으므로, HospitalForm.cs 파일이 프로젝트에 존재해야 합니다.
}