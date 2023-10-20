using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT2 : MonoBehaviour  {

	private List<Task> listCauHoi = new List<Task>() {
		new Task() {
			Question =
				"1. Điền từ còn thiếu vào chỗ trống: “Đảng Cộng sản Việt Nam là đội tiền phong của giai cấp công nhân, đồng thời là đội tiền phong của nhân dân lao động và của dân tộc Việt Nam, đại biểu trung thành lợi ích của giai cấp công nhân, nhân dân lao động và của dân tộc. Đảng lấy chủ nghĩa Mác - Lênin và Tư tưởng Hồ Chí Minh làm nền tảng tư tưởng, kim chỉ nam cho hành động, lấy ……làm nguyên tắc tổ chức cơ bản” (Văn kiện Đại hội XI của Đảng)",
			ListAnwser = new List<Answer>() {
				new Answer("a. Phê bình và tự phê bình", false),
				new Answer("b. Tập trung dân chủ", true),
				new Answer("c. Đoàn kết thống nhất trong Đảng", false),
				new Answer("d. Kỷ luật nghiêm minh, tự giác", false)
			}
		},

		new Task() {
			Question = "2. Đối tượng nghiên cứu của khoa học Lịch sử Đảng Cộng sản Việt Nam là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Các cán bộ, đảng viên Đảng Cộng sản Việt Nam ", false),
				new Answer("b. Những mặt hạn chế trong quá trình lãnh đạo của Đảng ", false),
				new Answer("c. Sự ra đời, phát triển và hoạt động lãnh đạo của Đảng qua các thời kỳ lịch sử ",
					true),
				new Answer("d. Các văn kiện của Đảng chuẩn bị được lưu hành ", false),
				// true
			}
		},

		new Task() {
			Question =
				"3. Là một ngành của khoa học lịch sử, Lịch sử Đảng Cộng sản Việt Nam có các chức năng, nhiệm vụ của khoa học lịch sử, đồng thời còn có thêm các chức năng  nổi bật khác là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Chức năng nhận thức, điều tiết, chọn lọc và tìm kiếm ", false),
				new Answer("b. Chức năng nhận thức, giáo dục, dự báo và phê phán", true),
				new Answer("c. Chức năng tuyên truyền, phổ cập, giáo huấn và phổ quát", false),
				new Answer("d. Chức năng giáo dục, sàng lọc, tuyên truyền và tìm kiếm   ", false),
				// true
			}
		},

		new Task() {
			Question =
				"4. Một trong những nhiệm vụ hàng đầu khi nghiên cứu khoa học Lịch sử Đảng Cộng sản Việt Nam là:",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Khẳng định, chứng minh giá trị khoa học của những mục tiêu chiến lược vàsách lược cách mạng mà Đảng đề ra trong cương lĩnh ",
					true),
				new Answer(
					"b. Làm cho người học hiểu được quyền lực của Đảng, từ đó thêm trung thành với đường lối lãnh đạo của Đảng ",
					false),
				new Answer(
					"c. Chọn lọc ra những sự kiện lịch sử nổi bật để tái hiện lại sự thành công trong quá trình lãnh đạo của Đảng",
					false),
				new Answer("d. Tìm hiểu về lịch sử ra đời của đảng cộng sản trên thế giới  ", false),
				// true
			}
		},

		new Task() {
			Question =
				"5. Trong phương pháp nghiên cứu, học tập môn học Lịch sử Đảng Cộng sản ViệtNam cần phải dựa trên phương pháp luận khoa học mác-xít, đồng thời phải nắvững chủ nghĩa nào dưới đây để xem xét và nhận thức lịch sử một cách khác quan, trung thực và đúng quy luật?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Chủ nghĩa duy vật biện chứng và chủ nghĩa duy vật lịch sử ", true),
				new Answer("b. Chủ nghĩa duy vật và chủ nghĩa duy tâm ", false),
				new Answer("c. Chủ nghĩa duy lý và chủ nghĩa duy vật biện chứng ", false),
				new Answer("d. Chủ nghĩa duy lý và chủ nghĩa duy vật lịch sử ", false),
				// true
			}
		},
		new Task() {
			Question =
				"6. Tại sao khi nghiên cứu, học tập Lịch sử Đảng Cộng sản Việt Nam lại cần phải nhận thức và vận dụng chủ nghĩa duy vật lịch sử?",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Để thấy được sự ưu việt của Đảng Cộng sản Việt Nam so với các đảng phái ở phương Tây ",
					false),
				new Answer("b. Để nhận thức tiến trình cách mạng do Đảng Cộng sản Việt Nam lãnh đạo", true),
				new Answer("c. Để hiểu được sứ mệnh lịch sử của giai cấp nông dân trong lãnh đạo cách mạng ",
					false),
				new Answer("d. Để hiểu vì sao cách mạng giải phóng dân tộc ở Việt Nam đi theo con đường tư sản ",
					false),
				// true
			}
		},
		new Task() {
			Question =
				"7. Trong nghiên cứu Lịch sử Đảng Cộng sản Việt Nam, khi xem xét, đối chiếu các hiện tượng lịch sử trong hình thức tổng quát nhằm mục đích vạch ra bản chất, quy luật, khuynh hướng chung trong sự vận động của sự vật thì đó là cách nghiên cứu dựa trên:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Phương pháp lịch sử ", false),
				new Answer("c. Phương pháp chọn lọc ", false),
				new Answer("c. Phương pháp làm việc nhóm ", false),
				new Answer("d. Phương pháp logic ", true),
				// true
			}
		},
		new Task() {
			Question =
				"8. Cần phải coi trọng phương pháp tổng kết thực tiễn lịch sử gắn với nghiên cứu lý luận trong nghiên cứu Lịch sử Đảng Cộng sản Việt Nam để:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Làm rõ kinh nghiệm, bài học, quy luật phát triển của cách mạng Việt Nam ", true),
				new Answer("b. Làm hài lòng người dân trong quá trình lãnh đạo cách mạng của Đảng ", false),
				new Answer("c. Dễ dàng thống kê những thành tựu mà Đảng đạt được trong lãnh đạo cách mạng", false),
				new Answer("d. Chứng tỏ sự linh hoạt trong các bước đề ra đường lối, chủ trương của Đảng ", false),
				// true
			}
		},
		new Task() {
			Question =
				"9. Việc tiến hành thảo luận, trao đổi các vấn đề do giảng viên đưa ra để có thể hiểu rõ hơn nội dung của môn học Lịch sử Đảng Cộng sản Việt Nam thì được gọi là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Phương pháp làm việc khách quan ", false),
				new Answer("b. Phương pháp làm việc nhóm ", true),
				new Answer("c. Phương pháp làm việc chủ quan ", false),
				new Answer("d. Phương pháp làm việc biện chứng ", false),
				// true
			}
		},
		new Task() {
			Question =
				"10. Một trong những ý nghĩa của việc nghiên cứu, học tập môn học Lịch sử Đảng Cộng sản Việt Nam đối với sinh viên là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Tích cực cổ vũ, tham gia vào quá trình “tự diễn biến”, “tự chuyển hoá” trongĐảng ",
					false),
				new Answer(
					"b. Giáo dục lý tưởng, truyền thống đấu tranh của Đảng, bồi đắp niềm tin vàosự lãnh đạo của Đảng ",
					true),
				new Answer(
					"c. Tin vào sự lãnh đạo của Đảng đưa đất nước tiến nhanh, mạnh, vững chắc theocon đường tư bản chủ nghĩa",
					false),
				new Answer(
					"d. Tham gia xây dựng cải cách, cải tổ Đảng theo mô hình của Đông Âu và Liên Xônhằm làm cho Đảng thêm vững mạnh ",
					false),
				// true
			}
		},
		new Task() {
			Question = "11. Mâu thuẫn cơ bản của xã hội Việt Nam kể từ khi Pháp xâm lược là gì?",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Mâu thuẫn giữa dân tộc ta với thực dân Pháp, mâu thuẫn giữa nông dânvới địa chủ phong kiến ",
					true),
				new Answer("b. Mâu thuẫn giữa công nhân với tư bản, mâu thuẫn giữa nông dân với địa chủphong kiến ",
					false),
				new Answer("c. Mâu thuẫn giữa nông dân với địa chủ phong kiến, mâu thuẫn giữa tư sản với vôsản ",
					false),
				new Answer("d. Mâu thuẫn giữa nông dân, công nhân với địa chủ phong kiến ", false),
				// true
			}
		},
		new Task() {
			Question =
				" 12. Ở Việt Nam, giai cấp mới nào đã ra đời dưới tác động của cuộc khai thác thuộcđịa lần thứ nhất (1897 - 1914) của thực dân Pháp?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Tư sản ", false),
				new Answer("b. Nông dân ", false),
				new Answer("c. Công nhân ", true),
				new Answer("d. Tiểu tư sản", false),

				// true
			}
		},
		new Task() {
			Question =
				"13. Trước khi thực dân Pháp nổ súng xâm lược (1858), xã hội Việt Nam có những giai cấp cơ bản nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Địa chủ phong kiến và nông dân ", true),
				new Answer("b. Địa chủ phong kiến và công nhân", false),
				new Answer("c. Công nhân và nông dân ", false),
				new Answer("d. Nông dân và tri thức ", false),
				// true
			}
		},
		new Task() {
			Question =
				"14. Các phong trào yêu nước ở Việt Nam trước khi có Đảng Cộng sản lãnh đạo có điểm chung là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Không nhận được sự ủng hộ của người dân, đặc biệt là giai cấp công - nông", false),
				new Answer("b. Không thông qua ý kiến của Quốc tế Cộng sản, đặc biệt Đảng Cộng sản Liên Xô", false),
				new Answer(
					"c. Không có đường lối rõ ràng dẫn đến thất bại và bị thực dân Pháp đàn ápmột cách nặng nề ",
					true),
				new Answer("d. Không có đủ tiềm lực tài chính và người đứng đầu lãnh đạo cách mạng", false),
				// true
			}
		},
		new Task() {
			Question = "15. Thực dân Pháp đã thực hiện chính sách gì về văn hoá xã hội để cai trị nước ta?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Ngu dân", true),
				new Answer("b. Bế quan toả cảng", false),
				new Answer("c. Đốt sách chôn Nho", false),
				new Answer("d. Chia để trị", false),
				// true
			}
		},

		new Task() {
			Question = "16. Tầng lớp tư sản mại bản của Việt Nam dưới thời Pháp thuộc có đặc điểm là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Có sự tham gia vào đời sống chính trị, kinh tế của chính quyền thực dânPháp", true),
				new Answer("b. Có tiềm lực kinh tế mạnh, là giai cấp đông đảo nhất trong xã hội", false),
				new Answer("c. Không có tư liệu sản xuất, phải bán sức lao động trong các nhà máy, xí nghiệp",
					false),
				new Answer("d. Chịu ba tầng áp bức, bóc lột: đế quốc, phong kiến và tư sản dân tộc ", false)
				// true
			}
		},

		new Task() {
			Question = "17. Vì sao tầng lớp tiểu tư sản lại không thể là lực lượng lãnh đạo cách mạngchống Pháp?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Vì địa vị kinh tế, chính trị của họ gắn chặt với Pháp", false),
				new Answer("b. Vì lực lượng này hoàn toàn không có mâu thuẫn về quyền lợi với thực dân Pháp",
					false),
				new Answer("c. Vì địa vị kinh tế của họ bấp bênh, thái độ hay dao động", true),
				new Answer("d. Vì lực lượng này nhận được nhiều cảm tình của thực dân Pháp", false),
				// true
			}
		},

		new Task() {
			Question =
				"18. Cuối thế kỷ XIX - đầu thế kỷ XX, nhiệm vụ hàng đầu cần phải được giải quyếtcấp thiết của cách mạng Việt Nam là:",
			ListAnwser = new List<Answer>() {
				new Answer(" a. Giải phóng dân tộc", true),
				new Answer("b. Đấu tranh giai cấp", false),
				new Answer("c. Canh tân đất nước", false),
				new Answer("d. Chia lại ruộng đất", false)
				// true
			}
		},

		new Task() {
			Question =
				"19. Sự kiện nào đã đánh dấu phong trào công nhân Việt Nam hoàn toàn trở thànhmột phong trào tự giác?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Năm 1920, khi tổ chức công hội ở Sài Gòn được thành lập ", false),
				new Answer("b. Năm 1925, khi cuộc bãi công ở nhà máy Ba Son diễn ra rầm rộ ", false),
				new Answer("c. Năm 1929, khi có sự ra đời của ba tổ chức cộng sản ", false),
				new Answer("d. Năm 1930, khi Đảng Cộng sản Việt Nam ra đời ", true),
				// true
			}
		},

		new Task() {
			Question = "20. Sự kiện nào được Nguyễn Ái Quốc đánh giá “như chim én nhỏ báo hiệu mùaxuân”?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Cách mạng Tháng Mười Nga bùng nổ và thắng lợi (1917) ", false),
				new Answer("b. Sự thành lập Đảng Cộng sản Pháp (1920)", false),
				new Answer("c. Vụ mưu sát viên toàn quyền Méc-Lanh của Phạm Hồng Thái (1924)", true),
				new Answer("d. Sự ra đời của Hội Việt Nam cách mạng Thanh niên (1925)", false),
				// true
			}
		},

		new Task() {
			Question =
				"21. Phong trào đình công, bãi công của công nhân Việt Nam trong những năm1926 - 1929 thuộc khuynh hướng nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Khuynh hướng phong kiến ", false),
				new Answer("b. Khuynh hướng dân chủ tư sản ", false),
				new Answer("c. Khuynh hướng vô sản  ", true),
				new Answer("d. Khuynh hướng dân chủ ", false),
				// true
			}
		},

		new Task() {
			Question =
				"22. Ai là người đại diện cho chủ trương đánh đuổi thực dân Pháp giành độc lập dântộc, khôi phục chủ quyền quốc gia bằng biện pháp bạo động?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Bùi Quang Chiêu ", false),
				new Answer("b. Phan Châu Trinh ", false),
				new Answer("c. Phan Bội Châu ", true),
				new Answer("d. Nguyễn Ái Quốc ", false),
				// true
			}
		},

		new Task() {
			Question =
				"23. Sự kiện nào đánh dấu bước ngoặt trong cuộc đời hoạt động cách mạng củaNguyễn Ái Quốc - từ người yêu nước trở thành người cộng sản?",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Bỏ phiếu tán thành việc gia nhập Quốc tế III và tham gia thành lập ĐảngCộng sản Pháp",
					true),
				new Answer(
					"b. Đọc bản Sơ thảo lần thứ nhất những luận cương về vấn đề dân tộc và vấn đềthuộc địa của Lênin ",
					false),
				new Answer("c. Gửi Bản yêu sách của nhân dân An Nam tới Hội nghị Véc-xây ", false),
				new Answer("d. Ra đi tìm đường cứu nước  ", false),
				// true
			}
		},

		new Task() {
			Question = "24. Hội Liên hiệp các dân tộc thuộc địa có cơ quan ngôn luận là tờ báo nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Thanh niên", false),
				new Answer("b. Cờ đo", false),
				new Answer("c. Độc lập", false),
				new Answer("d. Người cùng khổ ", true),
				// true
			}
		},

		new Task() {
			Question =
				"25. Nguyễn Ái Quốc đã đọc bản Sơ thảo lần thứ nhất những luận cương về vấn đề dân tộc và vấn đề thuộc địa của Lênin đăng trên báo Nhân đạo vào năm:",
			ListAnwser = new List<Answer>() {
				new Answer("a. 1919", false),
				new Answer("b. 1920", true),
				new Answer("c. 1921", false),
				new Answer("d. 1922  ", false),
				// true
			}
		},

		new Task() {
			Question =
				"26. Phong trào cách mạng Việt Nam vào cuối năm 1928, đầu năm 1929 đã hình thành làn sóng cách mạng nào dưới đây?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Cách mạng tư sản dân quyền", false),
				new Answer("b. Cách mạng dân tộc, dân chủ", true),
				new Answer("c. Cách mạng văn hoá", false),
				new Answer("d. Cách mạng tư sản  ", false),
				// true
			}
		},

		new Task() {
			Question =
				"27. Khẩu hiệu “Không thành công thì cũng thành nhân” được sử dụng trong cuộc khởi nghĩa nào dưới đây?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Ba Đình", false),
				new Answer("b. Bãi Sậy", false),
				new Answer("c. Yên Bái", false),
				new Answer("d. Hương Khê  ", false),
				// true
			}
		},

		new Task() {
			Question =
				"28. Tác phẩm nào của Nguyễn Ái Quốc đã vạch rõ âm mưu và thủ đoạn của chủ nghĩa đế quốc che giấu tội ác dưới cái vỏ bọc /“khai hoá văn minh/”?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Bản án chế độ thực dân Pháp", false),
				new Answer("b. Đường Kách mệnh", false),
				new Answer("c. Nhật ký trong tù", false),
				new Answer("d. Con rồng tre", false)
				// true
			}
		},
		new Task() {
			Question =
				"29. Hoạt động nào dưới đây của Nguyễn Ái Quốc có ý nghĩa là sự chuẩn bị về mặt tổ chức cho việc thành lập Đảng Cộng sản Việt Nam?",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Mở các lớp huấn luyện chính trị nhằm đào tạo cán bộ cho cách mạng Việt Nam(từ năm 1925 -1927) ",
					false),
				new Answer("b. Chủ trì Hội nghị hợp nhất các tổ chức cộng sản (2/1930) ", false),
				new Answer("c. Tham gia sáng lập Đảng Cộng sản Pháp (12/1920) ", false),
				new Answer("d. Thành lập Hội Việt Nam Cách mạng Thanh niên (6/1925)", true),
				// true
			}
		},
		new Task() {
			Question = "30. Cơ quan ngôn luận của Hội Việt Nam Cách mạng Thanh niên là tờ báo nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Người cùng khổ", false),
				new Answer("b. Lao động", false),
				new Answer("c. Công nhân  ", false),
				new Answer("d. Thanh niên ", true),
				// true
			}
		},
		new Task() {
			Question = "31. Sự kiện nào đánh dấu giai cấp công nhân Việt Nam đã bước đầu đi vào đấu tranh tự giác?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Bãi công của công nhân thợ nhuộm Sài Gòn - Chợ Lớn (1922) ", false),
				new Answer("b. Tổng bãi công của công nhân Bắc Kỳ (1922) ", false),
				new Answer("c. Bãi công của thợ máy xưởng Ba Son cảng Sài Gòn (1925)", true),
				new Answer("d. Bãi công của công nhân nhà máy sợi Nam Định (1930) ", false),
				// true
			}
		},
		new Task() {
			Question =
				"32. Tác phẩm nào dưới đây của Nguyễn Ái Quốc đã đề cập đến những vấn đề cơ bản của một cương lĩnh chính trị, chuẩn bị về tư tưởng, chính trị cho việc thành lập Đảng?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Bản án chế độ thực dân Pháp (1925) ", false),
				new Answer("b. Đường Kách mệnh (1927) ", true),
				new Answer("c. Đông Dương (1924) ", false),
				new Answer("d. Nhật ký trong tù (1943) ", false),
				// true
			}
		},
		new Task() {
			Question =
				"33. Tính chất và nhiệm vụ của cách mạng Việt Nam được thể hiện trong tác phẩm “Đường Kách mệnh” của Nguyễn Ái Quốc là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Cách mạng giải phóng dân tộc mở đường tiến lên chủ nghĩa xã hội", true),
				new Answer("b. Tư sản dân quyền và thổ địa cách mạng để đi tới xã hội cộng sản", false),
				new Answer("c. Canh tân đất nước theo xu hướng của Minh Trị duy tân ở Nhật", false),
				new Answer("d. Cách mạng xã hội chủ nghĩa để đi lên xã hội cộng sản", false),
				// true
			}
		},
		new Task() {
			Question = "34. Chi bộ Cộng sản thành lập ở Bắc Kỳ tháng 3/1929 nhằm mục đích gì?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Củng cố ảnh hưởng của Hội Việt Nam Cách mạng Thanh niên ", false),
				new Answer("b. Xây dựng đội ngũ cán bộ cho cách mạng, chuẩn bị Đại hội Đảng ", false),
				new Answer("c. Thành lập Đội Việt Nam tuyên truyền giải phóng quân ", false),
				new Answer("d. Chuẩn bị thành lập một đảng cộng sản thay thế Hội Việt Nam Cách mạng Thanh niên ",
					false),
				// true
			}
		},
		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
			}
		},

		new Task() {
			Question =
				"100. Để khẳng định địa vị pháp lý của Nhà nước Việt Nam Dân chủ Cộng hoà, Đảng đã chủ trương tổ chức hoạt động nào dưới đây?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Bầu cử toàn quốc để bầu ra Quốc hội và Chính phủ chính thức ", true),
				new Answer("b. Xây dựng các trụ sở làm việc trong khu vực tản cư ", false),
				new Answer("c. Đổi tên Đảng ta thành Liên Việt Cách mạng Đảng", false),
				new Answer("d. Đổi tên nước ta thành Cộng hoà xã hội chủ nghĩa Việt Nam ", false),
			}
		},
		new Task() {
			Question =
				"101. Cuộc Tổng tuyển cử toàn quốc đầu tiên của nước Việt Nam Dân chủ Cộng hoà được diễn ra vào thời gian nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. 5/1/1945", false),
				new Answer("b. 6/1/1946", true),
				new Answer("c. 7/1/1947 ", false),
				new Answer("d. 8/1/1948", false),
			}
		},

		new Task() {
			Question =
				"102. Hình thức bầu cử nào dưới đây được Đảng đề ra để người dân bầu Quốc hội và thành lập Chính phủ chính thức vào ngày 6/1/1946?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Giơ tay bầu trực tiếp", false),
				new Answer("b. Phổ thông đầu phiếu ", true),
				new Answer("c. Chấp chính quan ", false),
				new Answer("d. Đại cử tri đoàn ", false),
			}
		},
		new Task() {
			Question =
				"103. Điền từ còn thiếu vào chỗ trống để hoàn thành câu nói sau đây của Chủ tịch Hồ Chí Minh: “Các cơ quan của Chính phủ từ toàn quốc đến các làng, đều là …… của dân, nghĩa là để gánh việc chung cho dân, chứ không phải đè đầu dân như trong thời kỳ dưới quyền thống trị của Pháp, Nhật.” (Trích Thư gửi Uỷ ban nhân dân các kỳ, tỉnh, huyện và làng 17/10/1945)",
			ListAnwser = new List<Answer>() {
				new Answer("a. Công bộc ", true),
				new Answer("b. Bạn hữu ", false),
				new Answer("c. Đồng minh", false),
				new Answer("d. Giúp việc ", false),
			}
		},
		new Task() {
			Question = "104. Quốc hội thông qua bản Hiến pháp đầu tiên của nước Việt Nam Dân chủ Cộng hoà vào năm:",
			ListAnwser = new List<Answer>() {
				new Answer("a. 1946 ", true),
				new Answer("b. 1954 ", false),
				new Answer("c. 1975", false),
				new Answer("d. 1992 ", false),
			}
		},
		new Task() {
			Question = "105. Bản Hiến pháp đầu tiên của nước Việt Nam Dân chủ Cộng hoà do ai chủ trì soạn thảo?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Nguyễn Văn Tố ", false),
				new Answer("b. Võ Nguyên Giáp ", false),
				new Answer("c. Huỳnh Thúc Kháng", false),
				new Answer("d. Hồ Chí Minh ", true),
			}
		},
		new Task() {
			Question =
				"106. Trước yêu cầu tăng cường lực lượng cho cách mạng và tập trung chống Pháp ở Nam Bộ đã dẫn đến sự ra đời của tổ chức:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Hội Liên hiệp Quốc dân Việt Nam ", true),
				new Answer("b. Liên hiệp các tổ chức hữu nghị Việt Nam ", false),
				new Answer("c. Hội Liên hiệp các dân tộc bị áp bức ở Á Đông", false),
				new Answer("d. Hội Liên hiệp thuộc địa ở Pa-ri", false),
			}
		},
		new Task() {
			Question = "107. Sau ngày bầu cử Quốc hội (1/1946), nước ta đã làm gì để xây dựng chính quyền địa phương?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Thành lập các đơn vị lực lượng vũ trang mới", false),
				new Answer("b. Thành lập các tổ du kích làm nhiệm vụ bảo đảm an ninh", false),
				new Answer("c.  Bầu cử Hội đồng nhân dân các cấp", true),
				new Answer("d.  Thành lập đội dân quân tự vệ ở các làng xã", false),
			}
		},
		new Task() {
			Question =
				"108. Phiên họp đầu tiên của Quốc hội khóa I nước Việt Nam Dân chủ Cộng hoà (2/3/1946) đã được diễn ra tại:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Nhà hát lớn Hà Nội", true),
				new Answer("b. Nhà văn hóa thiếu nhi Hà Nội", false),
				new Answer("c.  Sân khấu kịch Hà Nội", false),
				new Answer("d.  Quảng trường Ba Đình", false),
			}
		},

		new Task() {
			Question =
				"109. Một trong những biện pháp củng cố lực lượng vũ trang cách mạng được Chính phủ lâm thời đề ra sau ngày bầu cử Quốc hội là: ",
			ListAnwser = new List<Answer>() {
				new Answer("a. Tích cực mua sắm vũ khí, lương thực", true),
				new Answer("b. Giải tán các đội dân quân tự vệ địa phương", false),
				new Answer("c.  Sát nhập quân đội Việt Nam vào quân đội Hoàng gia Anh", false),
				new Answer("d.  Thực hiện các cuộc diễn tập hải quân trên quy mô lớn với các nước xã hội chủnghĩa",
					false),
			}
		},
		new Task() {
			Question =
				"110. Để xây dựng chính quyền thực sự trong sạch, vững mạnh, Chủ tịch Hồ Chí Minh yêu cầu cán bộ các cấp chính quyền phải:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Duy trì lối làm việc theo hướng tả khuynh, giáo điều trong công sở", false),
				new Answer("b. Khắc phục và bỏ ngay những thói hư, tật xấu", true),
				new Answer("c.  Khai trừ ra khỏi Đảng những Đảng viên yếu kém về năng lực", false),
				new Answer("d.  Tạm dừng kiện toàn bộ máy chính quyền các cấp", false),
			}
		},
		new Task() {
			Question =
				"111. Sau ngày bầu cử (6/1/1946), Quốc hội mới đã tập trung vào công việc quan trọng nhất lúc bấy giờ là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Tổ chức những cuộc khai hoang, xây dựng cuộc sống mới cho dân nghèo", false),
				new Answer("b. Kêu gọi người dân góp tiền và hiện vật để ủng hộ cho ngân khố quốc gia", false),
				new Answer("c.  Tổ chức cuộc kháng chiến chống thực dân Pháp xâm lược ở Nam Bộ", true),
				new Answer("d.  Xây dựng hệ thống các trường đại học, cao đẳng hiện đại", false),
			}
		},
		new Task() {
			Question = "112. Hội đồng cố vấn Chính phủ Cách mạng lâm thời được thành lập năm 1945 do ai đứng đầu?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Vĩnh Thụy", true),
				new Answer("b. Phạm Văn Đồng", false),
				new Answer("c.  Hồ Chí Minh", false),
				new Answer("d.  Nguyễn Hải Thần", false),
			}
		},
		new Task() {
			Question = "115. Hội nghị toàn xứ Đảng bộ Nam Kỳ (ngày 25/10/1945) đã quyết định:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Đưa cách mạng Việt Nam rút lui vào hoạt động bí mật", false),
				new Answer("b. Tạm thời giải tán các tổ chức Đảng", false),
				new Answer("c.  Tiếp tục hòa hoãn, kéo dài thời gian với Pháp để xây dựng lực lượng", false),
				new Answer("d.  Củng cố lực lượng, kiên quyết đẩy lùi cuộc tấn công của quân Pháp", true),
			}
		},
		new Task() {
			Question = "116. “Thành đồng Tổ quốc” là danh hiệu Chủ tịch Hồ Chí Minh tặng nhân dân vùng nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Bắc Bộ", false),
				new Answer("b. Trung Bộ", false),
				new Answer("c.  Nam Bộ", true),
				new Answer("d.  Tây Nguyên", false),
			}
		},
		new Task() {
			Question =
				"117. Để làm thất bại âm mưu “Diệt Cộng, cầm Hồ, phá Việt Minh” của quân Tưởng và tay sai, Đảng và Chính phủ ta đã thực hiện sách lược:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Ký nhiều hòa ước có lợi cho quân Nhật", false),
				new Answer("b. Tiến hành các cuộc xung đột vũ trang với quân Tưởng", false),
				new Answer("c.  Đưa ra nhiều yêu sách đòi quân Tưởng phải rút quân khỏi Việt Nam", false),
				new Answer("d.  Triệt để lợi dụng mâu thuẫn kẻ thù, hòa hoãn, nhân nhượng có nguyên tắc với quân Tưởng",
					true),
			}
		},
		new Task() {
			Question = "118. Đâu là chính sách được Đảng và Chủ tịch Hồ Chí Minh dùng để đối phó với quân Tưởng?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Giao thiệp thân thiện, ứng xử mềm dẻo, linh hoạt với yêu sách của quân Tưởng và tay sai",
					true),
				new Answer("b. Mời Tưởng Giới Thạch làm chủ tịch Quốc hội nước Việt Nam Dân chủ Cộnghoà", false),
				new Answer("c.  Liên minh với quân đội Pháp để đuổi quân Tưởng về nước", false),
				new Answer("d.  Giao chính phủ cho quân đội của Tưởng, Đảng rút lui vào hoạt động bí mật", false),
			}
		},

		new Task() {
			Question =
				"119. Ngày 11/11/1945, Đảng chủ trương rút vào hoạt động bí mật bằng thông cáo tự giải tán, mục đích là để:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Đảng từ bỏ quyền lãnh đạo cách mạng Việt Nam", false),
				new Answer("b. Giao lại quyền lãnh đạo Đảng cho thực dân Pháp", false),
				new Answer("c.  Tránh mũi nhọn tấn công của Pháp và Tưởng", true),
				new Answer("d.  Thừa nhận sự cai trị hợp pháp của quân đội Tưởng ở Việt Nam", false),
			}
		},
		new Task() {
			Question =
				"120. Đảng chủ trương rút vào hoạt động bí mật năm 1945, chỉ để lại một bộ phận hoạt động công khai với danh nghĩa là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Hội nghiên cứu văn hóa Đông Dương", false),
				new Answer("b. Hội nghiên cứu chủ nghĩa Mác ở Đông Dương", true),
				new Answer("c.  Hội nhà báo Đông Dương", false),
				new Answer("d.  Hội những người yêu thiên nhiên Đông Dương", false),
			}
		},
		new Task() {
			Question =
				"121. Khẩu hiệu được nhân dân các tỉnh Nam Bộ dùng để nâng cao tinh thần chiến đấu trong những ngày đầu chống thực dân Pháp xâm lược lần thứ hai là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Thà chết tự do còn hơn sống nô lệ", true),
				new Answer("b. Quyết tử để Tổ quốc quyết sinh", false),
				new Answer("c.  Thóc không thiếu một cân, quân không thiếu một người", false),
				new Answer("d.  Vững tay súng, chắc tay cày", false),
			}
		},
		new Task() {
			Question =
				"122. Để hạn chế sự chống phá của các tổ chức chính trị tay sai thân Tưởng là Việt Quốc, Việt Cách, Đảng đã thực hiện chủ trương nào dưới đây?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Đồng ý bổ sung thêm 70 ghế trong Quốc hội cho các tổ chức này không qua bầu cử", true),
				new Answer("b. Điều động các đơn vị lực lượng vũ trang bao vây các tổ chức này, buộc rút quân nước về ",
					false),
				new Answer("c.  Cung cấp thuốc men, đạn dược và nhu yếu phẩm theo yêu cầu của các tổ chức", false),
				new Answer(
					"d.  Chấp nhận bỏ sử dụng đồng bạc Đông Dương, chuyển hoàn toàn sang sử dụng đồng tiền Quan kim, Quốc tệ",
					false),
			}
		},
		new Task() {
			Question = "123. Hiệp ước Trùng Khánh (Hiệp ước Hoa - Pháp) đã được ký kết vào năm nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. 1930", false),
				new Answer("b. 1946", true),
				new Answer("c.  1954", false),
				new Answer("d.  1975", false),
			}
		},
		new Task() {
			Question =
				"124. Bản chất của Hiệp ước Hoa - Pháp (28/2/1946) đã chà đạp lên nền độc lập của Việt Nam và hợp pháp hoá hành động xâm lược của:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Thực dân Pháp", true),
				new Answer("b. Quân đội Trung Quốc", false),
				new Answer("c.  Quân đội Nhật Bản", false),
				new Answer("d.  Đế quốc Mỹ", false),
			}
		},
		new Task() {
			Question = "125. Đảng chủ trương hòa hoãn với Pháp nhằm:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Tiêu diệt giặc nội xâm, phát triển kinh tế", false),
				new Answer("b. Có thời gian tìm các liên minh quân sự để đối phó với Pháp", false),
				new Answer("c.  Tiêu diệt tay sai, thúc đẩy nhanh quân Tưởng về nước", true),
				new Answer("d.  Lôi kéo sự ủng hộ của chính phủ Pháp", false),
			}
		},
		new Task() {
			Question = "126. Ngày 6/3/1946, Chủ tịch Hồ Chí Minh đã ký với Pháp văn bản nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Tạm ước", false),
				new Answer("b. Tạm ước sơ bộ", false),
				new Answer("c.  Hiệp ước sơ bộ", false),
				new Answer("d.  Hiệp định sơ bộ", true),
			}
		},

		new Task() {
			Question = "127. Hiệp ước Hoa - Pháp được ký kết ngày 28/2/1946 đã đẩy cách mạng Việt Nam vào tình thế:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Cùng lúc đối mặt trực tiếp với hai kẻ thù lớn là Pháp và Tưởng", true),
				new Answer("b. Chính phủ phải giao lại Quốc hội cho quân Tưởng", false),
				new Answer("c.  Quốc hội có nguy cơ giải tán", false),
				new Answer("d.  Nạn đói có thể bùng phát trở lại", false),
			}
		},
		new Task() {
			Question =
				"128. Một trong những nội dung của bản Hiệp định Sơ bộ được Hồ Chí Minh ký kết với Pháp ngày 6/3/1946 là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Thực dân Pháp có toàn quyền khai thác các mỏ khoáng sản ở Việt Nam trongthời hạn 30 năm",
					false),
				new Answer("b. Việt Nam đồng ý cho quân đội Pháp đóng quân ở các khu vực quân sự trọngđiểm", false),
				new Answer("c.  Quân Pháp phải rút quân dần ra khỏi Việt Nam trong thời hạn 5 năm", true),
				new Answer("d.  Việt Nam sẽ cung cấp lương thực, thuốc men và vũ khí cho quân đội Pháp đểđánh Tưởng",
					false),
			}
		},
		new Task() {
			Question =
				"129. Bản Hiệp định Sơ bộ được ký ngày 6/3/1946 có ý nghĩa to lớn với cách mạng Việt Nam bởi vì sẽ giúp chúng ta có thêm thời gian:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Chuẩn bị lực lượng mọi mặt cho cuộc kháng chiến với Pháp", true),
				new Answer("b. Xây dựng chủ nghĩa xã hội", false),
				new Answer("c.  Khai hoang các vùng đất phía Nam", false),
				new Answer("d.  Tổ chức bầu cử", false),
			}
		},
		new Task() {
			Question =
				"130. Chủ trương hoà hoãn, nhân nhượng với Pháp để kéo dài thời gian hoà bình xây dựng đất nước được thể hiện rõ trong bản Chỉ thị nào dưới đây?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Kháng chiến kiến quốc", false),
				new Answer("b. Hoà để tiến", true),
				new Answer("c.  Nhật - Pháp bắn nhau và hành động của chúng ta", false),
				new Answer("d.  Toàn dân kháng chiến", false),
			}
		},
		new Task() {
			Question =
				"131. Nguyên nhân sâu xa nào dẫn đến sự kiện Toàn quốc kháng chiến vào cuối năm 1946 của cách mạng Việt Nam?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Việt Nam đã có lực lượng hải quân và không quân đủ mạnh để chống Pháp", false),
				new Answer("b. Quân Pháp ở Việt Nam liên tục bội ước với mong muốn dâng nước ta cho Nhật", false),
				new Answer("c.  Quân Pháp ở Việt Nam đình chiến, kéo dài thời gian rút quân khỏi Việt Nam", false),
				new Answer(
					"d.  Quân Pháp ở Việt Nam bộc lộ rõ thái độ bội ước, quyết tâm muốn xâm lượcnước ta một lần nữa",
					true),
			}
		},
		new Task() {
			Question = "132. Tại Hà Nội, cuộc kháng chiến toàn quốc bắt đầu bằng sự kiện nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Loạt đại bác bắn vào thành Hà Nội từ pháo đài Láng", true),
				new Answer("b. Pháp đơn phương tuyên bố cắt đứt liên hệ với Chính phủ Việt Nam", false),
				new Answer("c.  Hồ Chí Minh ra lời kêu gọi Toàn quốc kháng chiến", false),
				new Answer("d.  Quân Pháp tấn công vào Đại Nội Huế", false),
			}
		},
		new Task() {
			Question = "133. Cuộc kháng chiến toàn quốc tại mặt trận Hà Nội (1946) kéo dài trong bao nhiêu ngày đêm?",
			ListAnwser = new List<Answer>() {
				new Answer("a. 54 ngày đêm", false),
				new Answer("b. 60 ngày đêm", true),
				new Answer("c.  72 ngày đêm", false),
				new Answer("d.  80 ngày đêm", false),
			}
		},
		new Task() {
			Question =
				"134. Đâu là phương châm kháng chiến được Đảng đề ra trong cuộc kháng chiến chống Pháp, giai đoạn 1945 - 1947?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Dựa trên sức mạnh toàn dân, tiến hành kháng chiến đánh nhanh, thắng nhanh", false),
				new Answer("b. Chiến đấu cầm chừng, tiến hành kháng chiến đánh chắc, tiến chắc", false),
				new Answer(
					"c.  Dựa trên sức mạnh toàn dân, tiến hành kháng chiến toàn dân, toàn diện, lâu dài và dựa vào sức mình là chính",
					true),
				new Answer("d.  Chiến đấu cầm chừng, tranh thủ thời gian xây dựng và phát triển kinh tế", false),
			}
		},
		new Task() {
			Question = "135. Tác phẩm “Kháng chiến nhất định thắng lợi” là do ai viết?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Hồ Chí Minh", false),
				new Answer("b. Phạm Văn Đồng", false),
				new Answer("c.  Võ Nguyên Giáp", false),
				new Answer("d.  Trường Chinh", true),
			}
		},
		new Task() {
			Question =
				"136. Tính đến năm 1950, những nhà nước nào đã công nhận và đặt quan hệ ngoại giao với Nhà nước Việt Nam Dân chủ Cộng hoà?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Trung Quốc, Liên Xô, Lào, Triều Tiên", false),
				new Answer("b. Trung Quốc, Liên Xô, các nước Đông Âu, Triều Tiên", true),
				new Answer("c.  Trung Quốc, Liên Xô, Lào, Campuchia", false),
				new Answer("d.  Trung Quốc, Liên Xô, các nước Đông Âu, Lào", false),
			}
		},
		new Task() {
			Question =
				"138. Trong Sắc lệnh về nghĩa vụ quân sự được Hồ Chí Minh ký ban hành tháng 11/1949 đã đề cập đến việc ưu tiên phát triển lực lượng ba thứ quân. Ba thứ quân đó gồm:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Bộ đội chủ lực, bộ đội địa phương, dân quân du kích", true),
				new Answer("b. Bộ đội chủ lực, bộ đội địa phương, thanh niên xung phong", false),
				new Answer("c.  Thanh niên xung phong, bộ đội địa phương, dân quân du kích", false),
				new Answer("d.  Thanh niên xung phong, bộ đội chủ lực, dân quân du kích", false),
			}
		},
		new Task() {
			Question =
				"137. Một trong những mục đích của Trung ương Đảng khi chủ động mở Chiến dịch Biên giới Thu Đông 1950 là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Mở rộng căn cứ địa Việt Bắc", true),
				new Answer("b. Khai thông biên giới Việt Nam - Lào - Campuchia", false),
				new Answer("c.  Ngăn chặn sự tiếp viện của Mỹ cho quân đội Pháp", false),
				new Answer("d.  Để giành thắng lợi quan trọng nhằm kết thúc chiến tranh với Pháp", false),
			}
		},
		new Task() {
			Question =
				"139. Chiến thắng trong Chiến dịch Biên giới Thu Đông năm 1950 có ý nghĩa lớn với cách mạng Việt Nam bởi vì:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Kết thúc thời kì chiến đấu trong vòng vây", true),
				new Answer("b. Pháp phải trao trả lại các vùng tạm chiếm cho quân đội Việt Nam", false),
				new Answer("c.  Đóng cửa hoàn toàn biên giới Việt Trung", false),
				new Answer("d.  Việt Nam được giải phóng hoàn toàn", false),
			}
		},
		new Task() {
			Question = "140. Đại hội đại biểu toàn quốc lần thứ II của Đảng (2/1951) đã diễn ra ở đâu?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Chiêm Hoá (Tuyên Quang)", true),
				new Answer("b. Võ Nhai (Thái Nguyên)", false),
				new Answer("c.  Bắc Sơn (Lạng Sơn)", false),
				new Answer("d.  Pác Bó (Cao Bằng)", false),
			}
		},
		new Task() {
			Question =
				"141. Đại hội đại biểu toàn quốc lần thứ II của Đảng (2/1951) đã quyết định việc Đảng ra hoạt động công khai và lấy tên là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Đảng Cộng sản Việt Nam", false),
				new Answer("b. Đảng Cộng sản Đông Dương", false),
				new Answer("c.  Đảng Lao động Việt Nam", true),
				new Answer("d.  Đông Dương Cộng sản Đảng", false),
			}
		},
		new Task() {
			Question = "142. Từ cuối năm 1950, Đảng quyết định tạm thời ngừng phát triển Đảng bởi vì:",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Ở nhiều nơi, việc phát triển Đảng quá nhanh dẫn đến việc mắc sai lầm về tiêu chuẩn Đảng viên",
					true),
				new Answer("b. Đảng muốn tập trung sức lực cho cuộc kháng chiến chống Pháp", false),
				new Answer("c.  Các Đảng viên tập trung vào làm kinh tế tư nhân, lơ là với nhiệm vụ cách mạng", false),
				new Answer("d.  Số lượng Đảng viên đã đủ", false),
			}
		},
		new Task() {
			Question = "143. Chiến dịch nào đã được Đảng phát động từ năm 1951?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Chiến dịch Tây Bắc Thu Đông", false),
				new Answer("b. Chiến dịch Hoà Bình", true),
				new Answer("c.  Chiến dịch Thượng Lào", false),
				new Answer("d.  Chiến dịch Nam Lào", false),
			}
		},
		new Task() {
			Question =
				"144. Động lực của cách mạng Việt Nam được xác định trong Đại hội đại biểu toàn quốc lần thứ II của Đảng (2/1951) gồm các giai cấp và tầng lớp nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Công nhân, nông dân, tiểu tư sản và tư sản mại bản", false),
				new Answer("b. Phong kiến, nông dân, công nhân và tư sản dân tộc", false),
				new Answer("c.  Công nhân, nông dân, tiểu tư sản và tư sản dân tộc", true),
				new Answer("d.  Phong kiến, nông dân, tư sản dân tộc và tư sản mại bản", false),
			}
		},

		new Task() {
			Question = "145. Đâu là mặt hạn chế của cuộc cải cách ruộng đất (1957) do Đảng phát động?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Chia ruộng đất chưa đồng đều cho người nông dân", false),
				new Answer("b. Độc đoán, quan liêu, gây ra oan sai không đáng có", true),
				new Answer("c.  Gây cản trở trong việc nâng cao năng suất cây trồng, vật nuôi", false),
				new Answer("d.  Không chia lại ruộng đất cho các gia đình bần, cố nông", false),
			}
		},
		new Task() {
			Question =
				"146. Tính chất của xã hội Việt Nam được xác định trong Đại hội đại biểu toàn quốc lần thứ II của Đảng (2/1951) là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Dân chủ nhân dân, một phần thuộc địa và nửa phong kiến", true),
				new Answer("b. Cộng sản chủ nghĩa, nửa phong kiến", false),
				new Answer("c.  Nửa phong kiến, nửa tư bản", false),
				new Answer("d.  Thuộc địa kiểu mới, nửa xã hội chủ nghĩa", false),
			}
		},
		new Task() {
			Question =
				"147. Thực dân Pháp dưới sự hỗ trợ tài chính của Mỹ, đã xây dựng Điện Biên Phủ trở thành một căn cứ quân sự khổng lồ và được giới quân sự, chính trị thế giới ca ngợi là một:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Pháo đài không thể công phá", true),
				new Answer("b. Cỗ máy không thể công phá", false),
				new Answer("c.  Trận địa thép không thể công phá", false),
				new Answer("d.  Căn cứ không thể công phá", false),
			}
		},
		new Task() {
			Question =
				"148. Ai đã được Bộ Chính trị giao chức vụ Tư lệnh kiêm Bí thư Đảng uỷ chiến dịch Điện Biên Phủ?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Hồ Chí Minh", false),
				new Answer("b. Nguyễn Chí Thanh", false),
				new Answer("c.  Nguyễn Vịnh", false),
				new Answer("d.  Võ Nguyên Giáp", true),
			}
		},

		new Task() {
			Question =
				"149. Chủ tịch Hồ Chí Minh đã nói: “Chiến dịch này là một chiến dịch rất quan trọng, không những về quân sự mà cả về chính trị, không những đối với trong nước mà đối với quốc tế. Vì vậy, toàn quân, toàn dân, toàn Đảng phải tập trung hoàn thành cho kỳ được”. Chiến dịch được Hồ Chí Minh nhắc đến trong câu nói trên là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Chiến dịch Hoàng Hoa Thám", false),
				new Answer("b. Chiến dịch Việt Bắc - Thu Đông 1947", false),
				new Answer("c.  Chiến dịch Hồ Chí Minh", false),
				new Answer("d.  Chiến dịch Điện Biên Phủ", true),
			}
		},
		new Task() {
			Question = "150. Phương châm kháng chiến của quân đội Việt Minh trong chiến dịch Điện Biên Phủ là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Đánh chắc, tiến chắc", true),
				new Answer("b. Thần tốc, bất ngờ", false),
				new Answer("c.  Táo bạo, thần tốc", false),
				new Answer("d.  Táo bạo, chắc thắng", false),
			}
		},
		new Task() {
			Question =
				"151. Trận Điện Biên Phủ (7/1954) thắng lợi đã mang lại ý nghĩa rất lớn không chỉ đối với cách mạng Việt Nam, mà còn cả đối với cách mạng thế giới vì:",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Báo hiệu sự thắng lợi của nhân dân các dân tộc bị áp bức, sự sụp đổ của chủ nghĩa thực dân",
					true),
				new Answer("b. Báo hiệu sự sụp đổ không thể tránh khỏi của chủ nghĩa tư bản", false),
				new Answer("c.  Làm sụp đổ hệ thống phát-xít ở các nước châu Âu", false),
				new Answer("d.  Giúp các quốc gia tư bản ở Châu Á - Âu - Mỹ Latinh tự giải phóng cho mình", false),
			}
		},
		new Task() {
			Question =
				"152. Một trong những ý nghĩa quan trọng của Hiệp định Giơ-ne-vơ (21/7/1954) đối với cách mạng Việt Nam là:",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Đánh dấu mốc cuộc kháng chiến chống Pháp kết thúc thắng lợi, miền Bắc được hoàn toàn giải phóng",
					true),
				new Answer(
					"b. Đánh dấu mốc cuộc kháng chiến chống Pháp kết thúc thắng lợi, miền Nam được hoàn toàn giải phóng",
					false),
				new Answer(
					"c.  Đánh dấu mốc cuộc kháng chiến chống Mỹ kết thúc thắng lợi, nước ta hoàn toàn được giải phóng",
					false),
				new Answer(
					"d.  Đánh dấu mốc cuộc kháng chiến chống Pháp bước sang một giai đoạn mới, nonsông thu về một mối, cả nước bước vào thời kỳ quá độ",
					false),
			}
		},
		new Task() {
			Question = "126. Ngày 6/3/1946, Chủ tịch Hồ Chí Minh đã ký với Pháp văn bản nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Tạm ước", false),
				new Answer("b. Tạm ước sơ bộ", true),
				new Answer("c.  Hiệp ước sơ bộ", false),
				new Answer("d.  Hiệp định sơ bộ", false),
			}
		},
		new Task() {
			Question =
				"153. Bản Hiệp định đình chỉ chiến sự ở Việt Nam (Hiệp định Giơ-ne-vơ) đã được các bên đồng ý kí kết vào thời gian nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. 27/1/1954", false),
				new Answer("b. 21/7/1954", true),
				new Answer("c.  21/1/1954", false),
				new Answer("d.  27/7/1954", false),
			}
		},
		new Task() {
			Question = "154. Sau ngày Hiệp định Giơ-ne-vơ được ký kết (7/1954), cách mạng miền Bắc có đặc điểm là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Trở thành thuộc địa kiểu mới của đế quốc Mỹ", false),
				new Answer("b. Được hoàn toàn giải phóng, phát triển theo con đường xã hội chủ nghĩa", false),
				new Answer("c.  Bị thực dân Pháp tái chiếm", false),
				new Answer("d.  Nằm dưới sự kiểm soát của chính quyền Ngô Đình Diệm", false),
			}
		},
		new Task() {
			Question =
				"155. Yếu tố nào dưới đây được xem như là kinh nghiệm của Đảng trong lãnh đạo kháng chiến chống Pháp và can thiệp Mỹ giai đoạn 1945 - 1954? ",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Kết hợp chặt chẽ và giải quyết đúng đắn mối quan hệ giữ hai nhiệm vụ cơ bản vừa kháng chiến vừa kiến quốc",
					true),
				new Answer(
					"b. Tập trung toàn bộ sức lực của toàn Đảng, toàn dân vào xây dựng và phát triển lực lượng vũ trang",
					false),
				new Answer(
					"c.  Có những sách lược ngoại giao khôn khéo để tranh thủ sự ủng hộ của các nước xã hội chủ nghĩa anh em",
					false),
				new Answer(
					"d.  Đề ra được phương châm xuyên suốt trong cuộc kháng chiến là lối đánh nhanh, thắng nhanh",
					false),
			}
		},
		new Task() {
			Question = "126. Ngày 6/3/1946, Chủ tịch Hồ Chí Minh đã ký với Pháp văn bản nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Tạm ước", false),
				new Answer("b. Tạm ước sơ bộ", false),
				new Answer("c.  Hiệp ước sơ bộ", false),
				new Answer("d.  Hiệp định sơ bộ", true),
			}
		},
		new Task() {
			Question =
				"156. Trong giai đoạn 1954 - 1965, Đảng đã gặp khó khăn gì trong lãnh đạo cách mạng ở hai miền Nam Bắc?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Thực dân Pháp vẫn chưa chịu rút quân về nước", true),
				new Answer("b. Đế quốc Mỹ mang quân ra miền Bắc Việt Nam", false),
				new Answer("c.  Cục diện Chiến tranh lạnh trên thế giới diễn ra rất căng thẳng", false),
				new Answer("d.  Đất nước ta bị chia làm hai miền với hai chế độ khác biệt", false),
			}
		},
		new Task() {
			Question =
				"157. Nguyên nhân nào dưới đây được xem là nguyên nhân gây ra một số sai lầm trong cải cách ruộng đất ở miền Bắc?",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Do chủ quan, giáo điều, không xuất phát từ tình hình thực tiễn ở nông thôn miền Bắc sau ngày giải phóng",
					true),
				new Answer("b. Do không có sự chuẩn bị kĩ lưỡng trước khi tiến hành cải cách", false),
				new Answer("c.  Do có sự chống đối quyết liệt từ người dân, đặc biệt là nông dân", false),
				new Answer("d.  Do miền Bắc còn nhiều ruộng đất bỏ hoang, vô chủ", false),
			}
		},
		new Task() {
			Question =
				"158. Một trong những kết quả nổi bật trong phát triển kinh tế - văn hoá và cải tạo xã hội chủ nghĩa ở miền Bắc trong ba năm (1958 - 1960) là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Miền Bắc có nền kinh tế tư bản phát triển cao, đời sống người dân được cải thiện rõ nét",
					false),
				new Answer(
					"b. Miền Bắc từng bước đi lên chủ nghĩa xã hội và trở thành hậu phương ổn định của tiền tuyến miền Nam",
					true),
				new Answer("c.  Nạn đói được đẩy lùi, người dân tự tổ chức những đợt di cư vào miền Nam", false),
				new Answer(
					"d.  Miền Bắc đã xây dựng được những cơ sở sản xuất vũ khí hiện đại, đáp ứng đủ cho nhu cầu của miền Nam",
					false),
			}
		},
		new Task() {
			Question =
				"159. Bản dự thảo “Đề cương cách mạng miền Nam” do đồng chí Lê Duẩn soạn thảo vào tháng 8/1956 được xem là một trong những văn kiện quan trọng bởi vì:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Góp phần hình thành đường lối cách mạng ở miền Nam của Đảng", true),
				new Answer("b. Giúp cách mạng miền Nam chuyển từ thế phòng ngự sang tiến công", false),
				new Answer("c.  Khích lệ tinh thần những người cộng sản đang bị giam giữ, tù đày", false),
				new Answer("d.  Cổ vũ người dân miền Nam đứng lên lật đổ chính quyền Ngô Đình Diệm", false),
			}
		},
		new Task() {
			Question =
				"160. Điền cụm từ còn thiếu vào chỗ trống: Hội nghị lần thứ 6 Ban Chấp hành Trung ương Đảng (từ ngày 15 đến 17/7/1954) đã chỉ rõ: “Hiện nay …… là kẻ thù chính của nhân dân thế giới, và nó đang trở thành kẻ thù chính và trực tiếp của nhân dân Đông Dương, cho nên mọi việc của ta đều nhằm chống ……”",
			ListAnwser = new List<Answer>() {
				new Answer("a. Phát-xít", false),
				new Answer("b. Thực dân Pháp", false),
				new Answer("c.  Đế quốc Mỹ", true),
				new Answer("d.  Chủ nghĩa tư bản", false),
			}
		},
		new Task() {
			Question =
				"161. Nghị quyết Bộ Chính trị tháng 9/1954 đã đề ra nhiệm vụ cụ thể, trước mắt chocách mạng miền Nam là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Bằng mọi cách yêu cầu quân đội Pháp rút hết quân về nước", false),
				new Answer("b. Giúp miền Bắc xây dựng chủ nghĩa xã hội nhanh, mạnh, vững chắc", false),
				new Answer(
					"c.  Tập hợp mọi lực lượng đấu tranh nhằm lật đổ chính quyền bù nhìn thân Mỹ, hoàn thành thống nhất Tổ quốc",
					true),
				new Answer("d.  Đề ra kế hoạch phát triển kinh tế miền Nam theo hướng tư bản chủ nghĩa", false),
			}
		},
		new Task() {
			Question = "162. Tháng 3/1959, chính quyền Ngô Đình Diệm đã:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Thông qua luật 10/59, đặt những người cộng sản ra khỏi vòng pháp luật", false),
				new Answer("b. Tuyên bố đặt miền Nam trong tình trạng chiến tranh", true),
				new Answer("c.  Mang quân ra xâm lược miền Bắc Việt Nam", false),
				new Answer("d.  Tự giải tán, đưa Ngô Đình Nhu lên cầm quyền", false),
			}
		},
		new Task() {
			Question = "163. Ngày 6/5/1959 chính quyền tay sai Ngô Đình Diệm đã ký ban hành Luật 10/59 nhằm mục đích:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Dùng toà án quân sự đặc biệt để đưa những người bị bắt ra xét xử và bắn giết tại chỗ",
					true),
				new Answer("b. Gây sức ép với Chính phủ Mỹ tăng viện trợ cho cuộc chiến ở miền Nam Việt Nam", false),
				new Answer("c.  Hợp pháp hoá việc mang quân ra miền Bắc Việt Nam", false),
				new Answer("d.  Chuẩn bị cho một cuộc đảo chính nhằm hất cẳng quân đội Mỹ ra khỏi miền Nam", false),
			}
		},
		new Task() {
			Question =
				"164. Dưới tác động của các chính sách khủng bố của chính quyền tay sai Ngô Đình Diệm, Hội nghị Trung ương lần thứ 15 của Đảng (1/1959) đã ra nghị quyết về cách mạng miền Nam với tinh thần cơ bản là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Tiếp tục cuộc cách mạng dân tộc dân chủ nhân dân", true),
				new Answer("b. Tạm thời dừng cuộc cách mạng dân tộc dân chủ nhân dân", false),
				new Answer("c.  Tiến hành các cuộc bạo động để giành lại chính quyền", false),
				new Answer("d.  Dừng sử dụng bạo lực cách mạng, chuyển hướng sang đấu tranh trên mặt trận ngoại giao",
					false),
			}
		},
		new Task() {
			Question = "165. Hình thức “Đồng Khởi” của nhân dân miền Nam (1960) lần đầu tiên diễn ra ở tỉnh:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Cà Mau", false),
				new Answer("b. Tiền Giang", false),
				new Answer("c.  Cần Thơ", false),
				new Answer("d.  Bến Tre", true),
			}
		},
		new Task() {
			Question =
				"166. Một trong những thủ đoạn được chính quyền tay sai Ngô Đình Diệm sử dụng để thi hành chính sách thực dân mới của Mỹ ở miền Nam Việt Nam là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Tăng cường thực hiện chính sách “tố cộng, diệt cộng”", true),
				new Answer("b. Lập ra chính quyền tự quản", false),
				new Answer("c.  Giải tán các ấp chiến lược", false),
				new Answer("d.  Giải tán các khu trù mật, khu dinh điền", false),
			}
		},
		new Task() {
			Question =
				"167. Mặt trận Dân tộc giải phóng miền Nam Việt Nam được thành lập ngày 20/12/1960 có ý nghĩa như là:",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Một tổ chức chính trị để tập hợp rộng rãi quần chúng nhân dân đoàn kết đấu tranh chống lại chính quyền độc tài của Ngô Đình Diệm",
					true),
				new Answer("b. Một tổ chức vũ trang với nhiệm vụ huấn luyện, đào tạo quân đội Việt Minh", false),
				new Answer("c.  Một tổ chức xã hội với nhiệm vụ chăm lo đời sống cho con em những người cộng sản",
					false),
				new Answer(
					"d.  Một tổ chức ngoại giao với nhiệm vụ kêu gọi sự ủng hộ của những người yêu chuộng hoà bình thế giới đối với cuộc kháng chiến chống Mỹ của nhân dân ta",
					false),
			}
		},
		new Task() {
			Question =
				"168. Phong trào nào dưới đây đã được phát triển mạnh và nhận được sự ủng hộ củahàng triệu người dân miền Nam trong giai đoạn đầu của cuộc kháng chiến chốngMỹ?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Đấu tranh đòi hiệp thương tổng tuyển cử", true),
				new Answer("b. Di dân ra miền Bắc Việt Nam", false),
				new Answer("c.  Dồn dân, lập ấp", false),
				new Answer("d.  Đưa cộng sản ra ngoài vòng pháp luật", false),
			}
		},
		new Task() {
			Question = "126. Ngày 6/3/1946, Chủ tịch Hồ Chí Minh đã ký với Pháp văn bản nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Tạm ước", false),
				new Answer("b. Tạm ước sơ bộ", false),
				new Answer("c.  Hiệp ước sơ bộ", true),
				new Answer("d.  Hiệp định sơ bộ", false),
			}
		},
		new Task() {
			Question =
				"169. Thắng lợi của phong trào Đồng khởi (1960) có ý nghĩa như thế nào đối với cách mạng miền Nam?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Khiến cho Mỹ phải xuống thang chiến tranh ở miền Nam", false),
				new Answer("b. Khiến cho Mỹ phải rút quân ra khỏi miền Nam Việt Nam", false),
				new Answer("c.  Chuyển cách mạng miền Nam từ thế giữ gìn lực lượng sang thế tiến công", false),
				new Answer("d.  Chuyển cách mạng miền Nam sang lối đánh thần tốc, táo bạo", true),
			}
		},
		new Task() {
			Question = "170. Đại hội đại biểu toàn quốc lần thứ III (9/1960) của Đảng họp ở đâu?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Tuyên Quang", true),
				new Answer("b. Hà Giang", false),
				new Answer("c.  Bắc Ninh", false),
				new Answer("d.  Hà Nội", false),
			}
		},
		new Task() {
			Question = "171. Chủ đề của Đại hội đại biểu toàn quốc lần thứ III của Đảng (9/1960) là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Xây dựng chủ nghĩa xã hội ở miền Bắc và đấu tranh hoà bình thống nhất nước nhà", true),
				new Answer("b. Cả nước bước vào thời kỳ quá độ, bỏ qua giai đoạn tư bản chủ nghĩa", false),
				new Answer("c.  Phát huy sức mạnh toàn dân tộc, đẩy mạnh công nghiệp hoá - hiện đại hoá", false),
				new Answer(
					"d.  Nâng cao năng lực lãnh đạo và sức chiến đấu của Đảng, sớm đưa nước ta ra khỏi tình trạng kém phát triển",
					false),
			}
		},
		new Task() {
			Question =
				"172. Đại Hội III của Đảng (9/1960) đã xác định nhiệm vụ của cách mạng Việt Nam trong giai đoạn mới là:",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Thực hiện cách mạng xã hội chủ nghĩa ở miền Bắc, cách mạng dân tộc dân chủ nhân dân ở miền Nam",
					true),
				new Answer("b. Làm nghĩa vụ quốc tế ở khu vực Đông Nam Á", false),
				new Answer("c.  Xoá bỏ những tàn tích phong kiến và nửa phong kiến làm cho người cày có ruộng", false),
				new Answer("d.  Phát triển chế độ dân chủ nhân dân, gây dựng cơ sở cho chủ nghĩa xã hội", false),
			}
		},
		new Task() {
			Question =
				"173. Đặc điểm nào dưới đây được Đại hội III của Đảng (9/1960) xem như là nổi bật nhất trong quá trình xây dựng chủ nghĩa xã hội ở miền Bắc thời kỳ 1961 - 1965?",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Từ một nền kinh tế nông nghiệp lạc hậu tiến thẳng lên chủ nghĩa xã hội, bỏ qua giai đoạn tư bản chủ nghĩa",
					true),
				new Answer("b. Từ một nền kinh tế nông nghiệp lạc hậu tiến thẳng lên chủ nghĩa tư bản", false),
				new Answer(
					"c.  Từ một nền kinh tế nông nghiệp lạc hậu, tiến hành công nghiệp hoá trên quy mô toàn miền",
					false),
				new Answer(
					"d.  Từ một nền kinh tế nông nghiệp lạc hậu chuyển sang xây dựng một nền kinh tế thương mại đa ngành",
					false),
			}
		},
		new Task() {
			Question = "174. Thành công to lớn nhất của Đại hội lần thứ III của Đảng (9/1960) là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Hoàn chỉnh đường lối chiến lược chung của cách mạng Việt Nam trong giai đoạn mới", true),
				new Answer("b. Đã lập ra nhà nước Việt Nam Dân chủ Cộng hoà", false),
				new Answer("c.  Lần đầu tiên Đảng đề ra bản Hiến pháp", false),
				new Answer("d.  Chuyển từ nền kinh tế quan liêu, bao cấp sang hạch toán kinh tế", false),
			}
		},

		new Task() {
			Question =
				"175. Kế hoạch 5 năm lần thứ nhất được Đại hội III của Đảng (9/1960) đề ra cho cách mạng miền Bắc được thực hiện vào thời gian nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. 1956 - 1960", false),
				new Answer("b. 1961 - 1965", true),
				new Answer("c.  1966 - 1970", false),
				new Answer("d.  1971 - 1975", false),
			}
		},
		new Task() {
			Question = "176. “Tàu không số” là khái niệm dùng để chỉ:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Những con tàu vận chuyển hàng hoá trái phép trong thời kì kháng chiến chống Mỹ", false),
				new Answer("b. Những con tàu do Liên Xô viện trợ để giúp Việt Nam chống Mỹ", false),
				new Answer(
					"c.  Những con tàu bí mật chở vũ khí, hàng hoá từ miền Bắc vào chi viện cho miền Nam chống Mỹ",
					true),
				new Answer(
					"d.  Những con tàu gián điệp Mỹ dùng để xâm nhập các cơ sở cách mạng bí mật ở miền Nam Việt Nam",
					false),
			}
		},
		new Task() {
			Question =
				"177. Chiến lược “Chiến tranh đặc biệt” của đế quốc Mỹ ở miền Nam đã diễn ra trong thời gian nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. 1961 - 1965", true),
				new Answer("b. 1960 - 1965", false),
				new Answer("c.  1965 - 1971", false),
				new Answer("d.  1971 - 1975", false),
			}
		},
		new Task() {
			Question =
				"178. Chiến thuật quân sự “trực thăng vận” và “thiết xa vận” được Mỹ áp dụng trong chiến lược chiến tranh nào ở miền Nam Việt Nam?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Chiến tranh đơn phương", false),
				new Answer("b. Chiến tranh cục bộ", false),
				new Answer("c.  Chiến tranh đặc biệt", true),
				new Answer("d.  Việt Nam hoá chiến tranh", false),
			}
		},
		new Task() {
			Question =
				"179. Một trong những kết quả miền Bắc đạt được sau 10 năm thực hiện khôi phục, cải tạo và xây dựng chế độ mới (1954 - 1964) là:",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Trở thành hậu phương vững chắc, đủ sức cung cấp nhân lực, tài lực, vật lực cho tiền tuyến miền Nam",
					true),
				new Answer("b. Trở thành thị trường xuất khẩu lúa gạo lớn nhất ở Đông Nam Á", false),
				new Answer("c.  Xây dựng được một nền công nghiệp hiện đại với cơ cấu đa ngành", false),
				new Answer("d.  Hoàn thành giai đoạn quá độ lên chủ nghĩa xã hội, chính thức bước vào xã hội cộng sản",
					false),
			}
		},
		new Task() {
			Question = "180. Ngày 15/2/1961, các lực lượng vũ trang ở miền Nam được thống nhất với tên gọi mới là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Đội giải phóng miền Nam Việt Nam", false),
				new Answer("b. Hội giải phóng miền Nam Việt Nam", false),
				new Answer("c.  Quân giải phóng miền Nam Việt Nam", true),
				new Answer("d.  Cục giải phóng miền Nam Việt Nam", false),
			}
		},
		new Task() {
			Question = "181. “Thóc không thiếu một cân, quân không thiếu một người” là khẩu hiệu dùng để chỉ:",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Sự chi viện nhiệt tình của hậu phương miền Bắc cho tiền tuyến miền Nam trong cuộc kháng chiến chống Mỹ",
					false),
				new Answer("b. Sự tự cung tự cấp trong đời sống hàng ngày của người dân miền Bắc", false),
				new Answer("c.  Sự phát triển về các mặt kinh tế, quân sự của miền Bắc", false),
				new Answer(
					"d.  Sự gian khổ trong đời sống của người dân miền Bắc trong những ngày chiến tranh phá hoại của đế quốc Mỹ",
					false),
			}
		},
		new Task() {
			Question = "126. Ngày 6/3/1946, Chủ tịch Hồ Chí Minh đã ký với Pháp văn bản nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Tạm ước", true),
				new Answer("b. Tạm ước sơ bộ", false),
				new Answer("c.  Hiệp ước sơ bộ", false),
				new Answer("d.  Hiệp định sơ bộ", false),
			}
		},
		new Task() {
			Question =
				"182. Tháng 12/1967, Bộ Chính trị đã ra một nghị quyết lịch sử, chuyển cuộc chiến tranh cách mạng miền Nam sang thời kỳ mới, thời kỳ tiến lên giành thắng lợi quyết định bằng phương pháp:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Mít-tinh, biểu tình", false),
				new Answer("b. Tổng công kích, tổng khởi nghĩa", true),
				new Answer("c.  Đàm phán ngoại giao với chính Phủ Mỹ", false),
				new Answer("d.  Khởi nghĩa từng phần", false),
			}
		},
		new Task() {
			Question =
				"183. Cuộc tập kích chiến lược bằng máy bay B52 của Mỹ vào Hà Nội, Hải Phòng và một số tỉnh ở miền Bắc cuối năm 1972 đã diễn ra trong bao nhiêu ngày đêm?",
			ListAnwser = new List<Answer>() {
				new Answer("a. 10 ngày đêm", false),
				new Answer("b. 12 ngày đêm", true),
				new Answer("c.  15 ngày đêm", false),
				new Answer("d.  30 ngày đêm", false),
			}
		},
		new Task() {
			Question =
				"184. Sau thất bại của chiến lược “Chiến tranh cục bộ”, Mỹ đã dùng chiến lược nào thay thế để tiếp tục cuộc chiến tranh xâm lược thực dân mới của Mỹ ở miền Nam?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Chiến tranh đơn phương", false),
				new Answer("b. Chiến tranh đặc biệt", false),
				new Answer("c.  Chiến tranh cục bộ", false),
				new Answer("d.  Việt Nam hoá chiến tranh", true),
			}
		},
		new Task() {
			Question =
				"185. Đâu là điểm nổi bật của chiến lược Việt Nam hoá chiến tranh mà Mỹ thực hiện ở miền Nam Việt Nam (1969 - 1973)?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Sử dụng lính đánh thuê từ Úc và Thái Lan", false),
				new Answer("b. Sử dụng hoàn toàn quân lính Mỹ", false),
				new Answer("c.  Dùng người Việt Nam đánh người Việt Nam", true),
				new Answer("d.  Sử dụng quân tình nguyện quốc tế", false),
			}
		},
		new Task() {
			Question =
				"186. Điền từ thích hợp vào chỗ trống để hoàn thành đoạn thơ chúc tết năm 1969 của Chủ tịch Hồ Chí Minh: “Năm qua thắng lợi vẻ vang Năm nay tiền tuyến chắc càng thắng to Vì độc lập, vì tự do Đánh cho …… cút, đánh cho …… nhào.”",
			ListAnwser = new List<Answer>() {
				new Answer("a. Mỹ - Ngụy", true),
				new Answer("b. Mỹ - Mỹ", false),
				new Answer("c.  Giặc - Ngụy", false),
				new Answer("d.  Mỹ - giặc", false),
			}
		},
		new Task() {
			Question =
				"187. Năm 1971, quân và dân Việt Nam đã phối hợp với quân và dân Lào chủ động đánh bại cuộc hành quân quy mô lớn nào của Mỹ?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Ánh sáng sao", false),
				new Answer("b. Lam Sơn 719", false),
				new Answer("c.  Tây Sơn thần tốc", true),
				new Answer("d.  Gian-xơn-xi-ti", false),
			}
		},
		new Task() {
			Question =
				"188. Hiệp định về chấm dứt chiến tranh, lập lại hoà bình ở Việt Nam (Hiệp định Pa-ri) đã được kí kết vào thời gian nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. 27/1/1973", true),
				new Answer("b. 21/7/1973", false),
				new Answer("c.  21/7/1954", false),
				new Answer("d.  27/1/1954", false),
			}
		},
		new Task() {
			Question = "189. Cuộc tổng tiến công và nổi dậy mùa Xuân năm 1975 bắt đầu bằng chiến dịch nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Chiến dịch Huế - Đà Nẵng", false),
				new Answer("b. Chiến dịch đường số 14", false),
				new Answer("c.  Chiến dịch đường 9 - Nam Lào", false),
				new Answer("d.  Chiến dịch Tây Nguyên", true),
			}
		},
		new Task() {
			Question = "190. Cuộc họp của Bộ chính trị ngày 18/3/1975 đã quyết định điều gì?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Đưa miền Bắc lên chủ nghĩa xã hội", false),
				new Answer("b. Kêu gọi Mỹ ngừng rải chất độc màu da cam xuống các cánh rừng miền Nam Việt Nam", false),
				new Answer("c.  Giải phóng miền Nam trong năm 1975", true),
				new Answer("d.  Xây dựng miền Nam Việt Nam trung tâm tài chính đất nước", false),
			}
		},
		new Task() {
			Question = "191. “Thần tốc, táo bạo, bất ngờ, chắc thắng” là tinh thần và khí thế của ta trong chiến dịch:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Điên Biên Phủ", false),
				new Answer("b. Huế - Đà Nẵng", false),
				new Answer("c.  Hồ Chí Minh", true),
				new Answer("d.  Tây Nguyên", false),
			}
		},
		new Task() {
			Question = "192. Ai là Tổng tư lệnh của cuộc Tổng tiến công và nổi dậy mùa Xuân 1975?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Hồ Chí Minh", false),
				new Answer("b. Nguyễn Chí Thanh", false),
				new Answer("c.  Phạm Văn Đồng", false),
				new Answer("d.  Võ Nguyên Giáp", true),
			}
		},
		new Task() {
			Question =
				"193. Hiệp định Pa-ri về chấm dứt chiến tranh, lập lại hoà bình ở Việt Nam đã tạo ra bước ngoặt mới trong cuộc kháng chiến chống Mỹ cứu nước của dân tộc Việt Nam vì từ Hiệp định này, quân Mỹ đã phải:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Rút quân khỏi Việt Nam, tạo thế xoay chuyển có lợi cho cách mạng", true),
				new Answer("b. Rút quân hoàn toàn ra khỏi miền Bắc Việt Nam", false),
				new Answer("c.  Ngừng ném bom tất cả các thành phố trọng điểm ở miền Nam", false),
				new Answer("d.  Bồi thường chiến phí cho các Cựu chiến binh Việt Nam", false),
			}
		},
		new Task() {
			Question = "194. Ngày 2/5/1975, những địa phương cuối cùng ở miền Nam được giải phóng là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Đồng bằng sông Cửu Long và các đảo, quần đảo ở Biển Đông", true),
				new Answer("b. Đông Nam Bộ và các đảo, quần đảo ở Biển Đông", false),
				new Answer("c.  Nam Trung Bộ và các đảo, quần đảo ở Biển Đông", false),
				new Answer("d.  Tây Nguyên và các đảo, quần đảo ở Biển Đông", false),
			}
		},
		new Task() {
			Question =
				"195. Yếu tố nào được xem là kinh nghiệm quý giá được rút ra từ sự lãnh đạo của Đảng trong cuộc kháng chiến chống Mỹ cứu nước giai đoạn 1954 - 1975?",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Giải quyết hài hoà nhiệm vụ của hai miền Nam Bắc trong bối cảnh nước ta bị chia cắt làm hai miền",
					true),
				new Answer("b. Luôn luôn duy trì lối đánh táo bạo, thần tốc, bất ngờ", false),
				new Answer("c.  Đảng phải huy động mọi tầng lớp, giai cấp tham gia vào mặt trận quân sự", false),
				new Answer("d.  Kiên trì mục tiêu giải phóng miền Nam trong trước năm 1975 trong mọi tình huống",
					false),
			}
		},
		new Task() {
			Question =
				"196. Ý nghĩa to lớn nhất về sự thắng lợi của cuộc kháng chiến chống Mỹ cứu nước (1954 - 1975) đối với cách mạng Việt Nam là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Giành lại nền độc lập, thống nhất, toàn vẹn lãnh thổ cho đất nước", true),
				new Answer("b. Đưa miền Nam Việt Nam trở thành khu đô thị kinh tế mới", false),
				new Answer("c.  Vị thế của Việt Nam trong khối ASEAN được nâng cao rõ rệt", false),
				new Answer("d.  Buộc Mỹ phải bồi thường chiến tranh cho Việt Nam", false),
			}
		},
		new Task() {
			Question =
				"197. Nguyên nhân mang tính quyết định tới sự thắng lợi của cuộc kháng chiến chống Mỹ cứu nước của nhân dân ta (1954 - 1975) là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Sự trợ giúp, hy sinh quên mình của hậu phương lớn miền Bắc xã hội chủ nghĩa", false),
				new Answer("b. Sự ủng hộ của các nước xã hội chủ nghĩa, đặc biệt là Liên Xô", false),
				new Answer("c.  Sự ủng hộ của các lực lượng yêu chuộng hoà bình trên thế giới", true),
				new Answer("d.  Sự ủng hộ của người dân Mỹ", false),
			}
		},
		new Task() {
			Question =
				"198. Điền cụm từ còn thiếu vào chỗ trống: “Năm tháng sẽ trôi qua nhưng thắng lợi của nhân dân ta trong sự nghiệp kháng chiến chống Mỹ cứu nước mãi mãi được ghi vào lịch sử dân tộc ta như một trong những trang chói lọi nhất, một biểu tượng sáng ngời về sự toàn thắng của …… và trí tuệ con người và đi vào lịch sử thế giới như một chiến công vĩ đại của thế kỉ XX, một sự kiện có tầm quan trọng quốc tế to lớn và có tính thời đại sâu sắc. Trích Bài diễn văn tại lễ khai mạc Đại hội IV của Đảng",
			ListAnwser = new List<Answer>() {
				new Answer("a. Chủ nghĩa xã hội chân chính", true),
				new Answer("b. Chủ nghĩa nhân đạo", false),
				new Answer("c.  Chủ nghĩa anh hùng cách mạng", false),
				new Answer("d.  Chủ nghĩa dân tộc nhược tiểu", false),
			}
		},
		new Task() {
			Question = "199. Hoàn cảnh lịch sử nổi bật của đất nước ta sau năm 1975 là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Đã có hoà bình, độc lập, thống nhất, cả nước quá độ lên chủ nghĩa xã hội", true),
				new Answer("b. Bị chia cắt làm hai miền, miền Bắc đi lên chủ nghĩa xã hội", false),
				new Answer("c.  Bị các nước trong khối xã hội chủ nghĩa bao vây, cấm vận", false),
				new Answer("d.  Đã cơ bản trở thành một nước công nghiệp theo hướng hiện đại", false),
			}
		},


		new Task() {
			Question =
				"270. Chiến tranh phá hoại miền Bắc lần thứ hai của đế quốc Mỹ diễn ra trong khoảng thời gian nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Từ 7/1954 đến 9/1955", false),
				new Answer("b. Từ 8/1961 đến 1/1962", false),
				new Answer("c. Từ 4/1972 đến 1/1973", true),
				new Answer("d. Từ 5/1975 đến 5/1976", false),
				// true
			}
		},
		new Task() {
			Question = "269. Hướng tiến công chủ yếu của quân ta trong cuộc Tiến công năm 1972 là",
			ListAnwser = new List<Answer>() {
				new Answer("a. Đông Nam Bộ", false),
				new Answer("b. Liên khu V", false),
				new Answer("c. Quảng Trị", true),
				new Answer("d. Tây Nguyên", false),
				// true
			}
		},
		new Task() {
			Question =
				"268. Trong cuộc tiến công chiến lược mùa khô 1966 - 1967, Mỹ đã tiến hành bao nhiêu cuộc hành quân chiến lược? a. 120 cuộc hành quân b. 500 cuộc hành quân c. 895 cuộc hành quân d. 700 cuộc hành quân",
			ListAnwser = new List<Answer>() {
				new Answer("a. 120 cuộc hành quân", false),
				new Answer("b. 500 cuộc hành quân", false),
				new Answer("c. 895 cuộc hành quân", true),
				new Answer("d. 700 cuộc hành quân", false),
				// true
			}
		},
		new Task() {
			Question = "267. Cuộc hành quân Lam Sơn 719 của Mỹ - Ngụy bị đánh bại vào năm nào? ",
			ListAnwser = new List<Answer>() {
				new Answer("a. 1951", false),
				new Answer("b. 1961", false),
				new Answer("c. 1971", true),
				new Answer("d. 1976", false),
				// true
			}
		},
		new Task() {
			Question = "266. Tính đến năm 1986, Đảng ta có bao nhiêu bản cương lĩnh?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Một", false),
				new Answer("b. Hai", false),
				new Answer("c. Ba", true),
				new Answer("d. Bốn", false),
				// true
			}
		},
		new Task() {
			Question =
				"265. Điền từ còn thiếu vào chỗ trống: “Đảng ta xứng đáng là lực lượng lãnh đạo Nhà nước và xã hội. Đất nước ta chưa bao giờ có được cơ đồ và vị thế như ngày nay. Đó là kết quả sự nỗ lực phấn đấu không ngừng của toàn Đảng, toàn dân, toàn quân, trong đó có sự hy sinh quên mình của ……. Chúng ta có quyền tự hào về bản chất tốt đẹp, truyền thống anh hùng và lịch sử vẻ vang của Đảng ta - Đảng của Chủ tịch Hồ Chí Minh vĩ đại, đại biểu của dân tộc Việt Nam anh hùng.” (Trích Nghị quyết Hội nghị lần thứ tư Ban Chấp hành Trung ương Đảng khoá XII)",
			ListAnwser = new List<Answer>() {
				new Answer("a. Công nhân, nông dân", false),
				new Answer("b. Cán bộ, đảng viên", true),
				new Answer("c. Tiểu thương, tri thức", false),
				new Answer("d. Công nhân, viên chức", false),
				// true
			}
		},
		new Task() {
			Question = "264. Cơ chế vận hành của hệ thống chính trị ở nước ta hiện nay là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Chính phủ lãnh đạo, Nhà nước quản lý, Đảng làm chủ", false),
				new Answer("b. Chính phủ lãnh đạo, người dân quản lý, Nhà nước làm chủ", false),
				new Answer("c. Đảng lãnh đạo, Nhà nước quản lý, nhân dân làm chủ", true),
				new Answer("d. Đảng lãnh đạo, nhân dân quản lý, Nhà nước làm chủ", false),
				// true
			}
		},
		new Task() {
			Question =
				"263. Trong xây dựng Đảng, nhân tố nào được xem là mang tính quyết định sự thành bại của cách mạng, là khâu then chốt? ",
			ListAnwser = new List<Answer>() {
				new Answer("a. Đào tạo nhân tài", false),
				new Answer("b. Bồi dưỡng trí thức", false),
				new Answer("c. Kết nạp đảng viên", false),
				new Answer("d. Công tác cán bộ", true),
				// true
			}
		},
		new Task() {
			Question =
				"262. Một trong những kinh nghiệm mà Đảng đã nhận thức sâu sắc trong quá trình hơn 30 năm lãnh đạo công cuộc đổi mới là: ",
			ListAnwser = new List<Answer>() {
				new Answer("a. Quán triệt tinh thần lấy dân làm gốc, vì lợi ích của nhân dân", true),
				new Answer("b. Thay đổi mục tiêu độc lập dân tộc và chủ nghĩa xã hội", false),
				new Answer("c. Làm từng phần, chia nhỏ từng ngành, từng lĩnh vực", false),
				new Answer("d. Đặt lợi ích của Đảng, của Nhà nước lên trên hết", false),
				// true
			}
		},
		new Task() {
			Question =
				"261. Thành tựu nào dưới đây của Việt Nam trong những năm vừa qua đã được Liên Hợp Quốc và cộng đồng quốc tế công nhận, đánh giá cao?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Bảo vệ môi trường", false),
				new Answer("b. Đột phá trong giáo dục", false),
				new Answer("c. Giảm nghèo", true),
				new Answer("d. Giảm tệ nạn xã hội", false),
				// true
			}
		},
		new Task() {
			Question =
				"260. Một trong những thành tựu quan trọng nhất mà Đảng và nước ta đã đạt được trong hơn 30 năm tiến hành công cuộc đổi mới là:",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Đã xây dựng và hoàn thiện từng bước nền kinh tế thị trường định hướng xã hội chủ nghĩa",
					true),
				new Answer("b. Đã cơ bản hoàn thành việc phổ cập giáo dục bậc Đại học trong nhân dân", false),
				new Answer("c. Xoá bỏ hoàn toàn sự phân hoá giàu nghèo ", false),
				new Answer("d. Đa số người dân hiện nay đều sống ở khu vực thành thị", false),
				// true
			}
		},
		new Task() {
			Question =
				"259. Tổ chức nào dưới đây là cơ quan thường trực của Ban Chỉ đạo Trung ương về phòng, chống tham nhũng?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Ban Kinh tế", false),
				new Answer("b. Ban Nội chính Trung ương", true),
				new Answer("c. Uỷ ban kiểm tra Trung ương", false),
				new Answer("d. Văn phòng Chính phủ", false),
				// true
			}
		},
		new Task() {
			Question =
				"258. Đâu là vấn đề được hội nghị Trung ương 4 khoá XI (1/2012) coi là nhiệm vụ cơ bản, lâu dài, phải thực hiện thường xuyên, có hiệu quả",
			ListAnwser = new List<Answer>() {
				new Answer("a. Thay đổi tên Đảng", false),
				new Answer("b. Thay đổi giai cấp lãnh đạo cách mạng cho phù hợp ", false),
				new Answer("c. Tự diễn biến, tự chuyển hoá", false),
				new Answer("d. Phòng chống tham nhũng", true),
				// true
			}
		},
		new Task() {
			Question =
				"257. Quan điểm chỉ đạo của Đảng trong Nghị quyết Trung ương 4 khoá X đã chỉ rõ yếu tố nào dưới đây là bộ phận cấu thành chủ quyền thiêng liêng của Tổ quốc, là không gian sinh tồn, cửa ngõ giao lưu quốc tế?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Đất", false),
				new Answer("b. Biển", true),
				new Answer("c. Biên giới", false),
				new Answer("d. Rừng", false),
				// true
			}
		},
		new Task() {
			Question = "256. Đâu là mục tiêu Thiên niên kỷ mà Việt Nam đã hoàn thành trước năm 2015?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Xoá bỏ tình trạng nghèo đói cùng cực", true),
				new Answer("b. Tăng cường bình đẳng giới và nâng cao vị thế cho phụ nữ", false),
				new Answer("c. Nâng cao sức khoẻ bà mẹ trẻ em", false),
				new Answer("d. Ngăn chặn HIV/AIDS, sốt rét và các bệnh dịch khác", false),
				// true
			}
		},
		new Task() {
			Question = "255. Trong xây dựng văn hoá, trọng tâm là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Tăng cường dạy ngoại ngữ cho nhân dân", false),
				new Answer("b. Xây dựng thêm các chùa để mở rộng không gian sinh hoạt tâm linh", false),
				new Answer("c. Chăm lo xây dựng con người có nhân cách và lối sống tốt đẹp", true),
				new Answer("d. Tuyên truyền, phổ biến đạo Phật vào trong đời sống người dân", false),
				// true
			}
		},
		new Task() {
			Question =
				"254. Quan điểm chủ đạo của Hội nghị Trung ương 9 khoá XI đã coi văn hoá phải được đặt ngang hàng với: ",
			ListAnwser = new List<Answer>() {
				new Answer("a. Kinh tế, chính trị, xã hội", true),
				new Answer("b. Khoa học, công nghệ và giáo dục", false),
				new Answer("c. Y tế, giáo dục", false),
				new Answer("d. Khoa học, giáo dục", false),
				// true
			}
		},
		new Task() {
			Question =
				"253. Theo số liệu từ Tổng cục Thống kê, GDP bình quân đầu người năm 2010 của Việt Nam đạt:",
			ListAnwser = new List<Answer>() {
				new Answer("a. 1.100USD", true),
				new Answer("b. 2.100USD", false),
				new Answer("c. 3.100USD", false),
				new Answer("d. 4.100USD", false),
				// true
			}
		},
		new Task() {
			Question =
				"252. Trong Hội nghị nhìn lại mười năm thực hiện Chiến lược phát triển kinh tế - xã hội 2001 - 2010, Đảng đã rút ra bài học kinh nghiệm sâu sắc. Đó là: ",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Trong bất kỳ điều kiện, tình huống nào cũng phải luôn kiên trì thực hiện đường lối và mục tiêu đổi mới",
					true),
				new Answer("b. Luôn phải có sự sáng tạo, thay đổi chủ nghĩa Mác - Lênin, Tư tưởng Hồ Chí Minh",
					false),
				new Answer(
					"c. Thay đổi mục tiêu độc lập dân tộc và chủ nghĩa xã hội cho phù hợp với hoàn cảnh mới",
					false),
				new Answer("d. Ưu tiên tăng trưởng kinh tế so với việc thực hiện tiến bộ và công bằng xã hội",
					false),
				// true
			}
		},
		new Task() {
			Question = "251. Trong năm 2012, ASEAN và Trung Quốc đã xây dựng Tuyên bố chung về điều gì? ",
			ListAnwser = new List<Answer>() {
				new Answer("a. Phân định biên giới trên bộ", false),
				new Answer("b. Ứng xử của các bên ở biển Đông", true),
				new Answer("c. Hợp tác toàn diện về thương mại", false),
				new Answer("d. Phân định lãnh hải", false),
				// true
			}
		},
		new Task() {
			Question =
				"250. Điền từ thích hợp vào chỗ trống: “…… là tổ chức liên minh chính trị, tổ chức xã hội và các cá nhân tiêu biểu trong các giai cấp, tầng lớp xã hội, các dân tộc, các tôn giáo và người Việt Nam định cư ở nước ngoài, là một bộ phận của hệ thống chính trị, là cơ sở chính trị của chính quyền nhân dân.”",
			ListAnwser = new List<Answer>() {
				new Answer("a. Hội Chữ thập đỏ Việt Nam", false),
				new Answer("b. Hội Liên hiệp Phụ nữ Việt Nam", false),
				new Answer("c. Mặt trận Tổ quốc Việt Nam", true),
				new Answer("d. Hội Cựu chiến binh Việt Nam", false),
				// true
			}
		},
		new Task() {
			Question =
				"249. Điền từ còn thiếu vào chỗ trống: Chủ đề của Đại hội XII của Đảng (2016) là: “Tăng cường xây dựng Đảng trong sạch, vững mạnh; phát huy sức mạnh toàn dân tộc, dân chủ xã hội chủ nghĩa, đẩy mạnh toàn diện, đồng bộ công cuộc đổi mới; bảo vệ vững chắc Tổ quốc, giữ vững môi trường hoà bình, ổn định; phấn đấu sớm đưa nước ta cơ bản trở thành: …….” ",
			ListAnwser = new List<Answer>() {
				new Answer("a. Nước có nền nông nghiệp phát triển", false),
				new Answer("b. Nước công nghiệp theo hướng hiện đại", true),
				new Answer("c. Nước có nền kinh tế dựa vào lâm nghiệp", false),
				new Answer("d. Nước có tốc độ phát triển kinh tế nhanh", false),
				// true
			}
		},
		new Task() {
			Question =
				"248. Trong Cương lĩnh 2011, Đảng đã xác định yếu tố nào dưới đây nếu không kịp thời ngăn chặn sẽ dẫn đến những tổn thất khôn lường đối với vận mệnh của đất nước, của chế độ xã hội chủ nghĩa và của Đảng?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Quan liêu, mất đoàn kết nội bộ, tả khuynh", false),
				new Answer("b. Giáo điều, tham nhũng, cậy quyền", false),
				new Answer("c. Quan liêu, tham nhũng, xa rời nhân dân", true),
				new Answer("d. Xa rời nhân dân, tả khuynh, tham nhũng", false),
				// true
			}
		},
		new Task() {
			Question =
				"247. Nghị quyết “Về đổi mới căn bản, toàn diện giáo dục đào tạo” đã được thông qua vào năm nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. 2010", false),
				new Answer("b. 2013", true),
				new Answer("c. 2015", false),
				new Answer("d. 2017", false),
				// true
			}
		},
		new Task() {
			Question =
				"246. Điền từ thích hợp vào chỗ trống: “Trong công tác tư tưởng, lý luận, ……. là lĩnh vực trọng yếu để xây dựng, bồi đắp nền tảng chính trị của chế độ, là tiếng nói của Đảng, Nhà nước, của các tổ chức chính trị - xã hội và là diễn đàn của nhân lOMoARcPSD|31518937 dân, đặt dưới sự lãnh đạo trực tiếp của Đảng, sự quản lý của Nhà nước và hoạt động trong khuôn khổ của pháp luật.",
			ListAnwser = new List<Answer>() {
				new Answer("a. Văn nghệ", false),
				new Answer("b. Nghệ thuật", false),
				new Answer("c. Báo chí", true),
				new Answer("d. Truyền thông", false),
				// true
			}
		},
		new Task() {
			Question =
				"245. “Hiệp ước về biên giới trên đất liền Việt Nam - Trung Quốc” được ký kết vào năm nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. 1989", false),
				new Answer("b. 1999", true),
				new Answer("c. 2009", false),
				new Answer("d. 2019", false),
				// true
			}
		},
		new Task() {
			Question =
				"244. Trong giai đoạn 2006 - 2010, quốc gia nào là đối tác thương mại lớn nhất của Việt Nam?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Mỹ", false),
				new Answer("b. Nhật", false),
				new Answer("c. Liên minh Châu Âu", false),
				new Answer("d. Trung Quốc", true),
				// true
			}
		},
		new Task() {
			Question =
				"243. Đến năm 2010, Việt Nam có quan hệ thương mại đầu tư với bao nhiêu quốc gia và vùng lãnh thổ? ",
			ListAnwser = new List<Answer>() {
				new Answer("a. 200", false),
				new Answer("b. 230", true),
				new Answer("c. 250", false),
				new Answer("d. 270", false),
				// true
			}
		},
		new Task() {
			Question =
				"242. Việt Nam chính thức trở thành thành viên thứ 150 của Tổ chức thương mại thế giới (WTO) vào năm nào? ",
			ListAnwser = new List<Answer>() {
				new Answer("a. 2000", false),
				new Answer("b. 2002", false),
				new Answer("c. 2004", false),
				new Answer("d. 2006", true),
				// true
			}
		},
		new Task() {
			Question =
				"241. Lực lượng nào trong xã hội được Đảng ta xem như là rường cột của nước nhà và là một trong những nhân tố quyết định sự thành bại của sự nghiệp công nghiệp hoá, hiện đại hoá đất nước?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Thanh niên", true),
				new Answer("b. Thiếu niên", false),
				new Answer("c. Phụ nữ", false),
				new Answer("d. Cựu chiến binh", false),
				// true
			}
		},
		new Task() {
			Question =
				"240. Theo Nghị quyết Trung ương 4 khoá X (4 /2007), Chính phủ Việt Nam có bao nhiêu bộ và cơ quan ngang bộ?",
			ListAnwser = new List<Answer>() {
				new Answer("a. 22", true),
				new Answer("b. 40", false),
				new Answer("c. 45", false),
				new Answer("d. 60", false),
				// true
			}
		},
		new Task() {
			Question =
				"239. Hội nghị Trung ương 8 của Đảng (1995) đã đề ra các biện pháp tăng cường pháp chế xã hội chủ nghĩa, theo đó Nhà nước sẽ quản lý xã hội bằng:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Đạo luật", false),
				new Answer("b. Sắc lệnh", false),
				new Answer("c. Pháp luật", true),
				new Answer("d. Chỉ thị", false),
				// true
			}
		},
		new Task() {
			Question =
				"238. Theo số liệu từ Ngân hàng Thế giới, Việt Nam đã ra khỏi tình trạng nước nghèo vào năm nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. 2008", true),
				new Answer("b. 2009", false),
				new Answer("c. 2010", false),
				new Answer("d. 2011", false),
				// true
			}
		},
		new Task() {
			Question = "237. Quan điểm mới nổi bật Đại hội X trong vấn đề Đảng viên là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Cho phép Đảng viên tranh cử ở nước ngoài", false),
				new Answer("b. Cho phép Đảng viên làm kinh tế tư nhân", true),
				new Answer("c. Cho phép Đảng viên có nhiều hơn một quốc tịch", false),
				new Answer("d. Cho phép Đảng viên tham gia nhiều tổ chức cơ sở Đảng cùng một lúc", false),
				// true
			}
		},
		new Task() {
			Question = "236. Quan điểm của Đảng và Nhà nước ta trong vấn đề tôn giáo là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Khuyến khích người dân tham gia đạo Phật", false),
				new Answer("b. Công nhận truyền thống cúng ông bà tổ tiên là tôn giáo chính thống", false),
				new Answer("c. Tôn trọng và bảo đảm quyền tự do tín ngưỡng", true),
				new Answer("d. Khuyến khích người dân tham gia vào các tổ chức tôn giáo", false),
				// true
			}
		},
		new Task() {
			Question =
				"235. Hội nghị Trung ương 7 (3/2003) đã thống nhất nhận thức coi yếu tố nào dưới đây là tài nguyên quốc gia vô cùng quý giá, là tư liệu sản xuất đặc biệt, là nguồn nội lực và nguồn vốn to lớn của đất nước? ",
			ListAnwser = new List<Answer>() {
				new Answer("a. Khoáng sản", false),
				new Answer("b. Đất đai", true),
				new Answer("c. Rừng nguyên sinh", false),
				new Answer("d. Hệ sinh thái", false),
				// true
			}
		},
		new Task() {
			Question = "234. Tại Đại hội X, Đảng đã lần đầu tiên đặt chú trọng đến nhiệm vụ:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Xây dựng nhà nước pháp quyền", false),
				new Answer("b. Xây dựng, chỉnh đốn Đảng", true),
				new Answer("c. Xây dựng nền văn hoá Việt Nam tiên tiến, đậm đà bản sắc dân tộc", false),
				new Answer("d. Đổi mới căn bản, toàn diện giáo dục", false),
				// true
			}
		},
		new Task() {
			Question =
				"233. Chủ đề của Đại hội X của Đảng (2006) là: “Nâng cao năng lực lãnh đạo và sức chiến đấu của Đảng, phát huy sức mạnh toàn dân tộc, đẩy mạnh toàn diện công cuộc đổi mới, sớm đưa nước ta .……” . Từ còn thiếu trong chỗ trống là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Trở thành nước công nghiệp hiện đại", false),
				new Answer("b. Thoát khỏi nhóm quốc gia nghèo", false),
				new Answer("c. Ra khỏi tình trạng kém phát triển", true),
				new Answer("d. Gia nhập nhóm các nước phát triển", false),
				// true
			}
		},
		new Task() {
			Question =
				"232. Hội nghị Trung ương 5 (3/2002) đã thảo luận và thống nhất nhận thức coi yếu tố nào dưới đây là bộ phận cấu thành quan trọng của nền kinh tế quốc dân?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Doanh nghiệp nhà nước", false),
				new Answer("b. Kinh tế tập thể", false),
				new Answer("c. Kinh tế có vốn đầu tư nước ngoài", false),
				new Answer("d. Kinh tế tư nhân", true),
				// true
			}
		},
		new Task() {
			Question =
				"231. Đại hội IX của Đảng đã đề ra Chiến lược phát triển kinh tế - xã hội 10 năm tiếp theo (2001 - 2010) với mục tiêu tổng quát là:",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Tạo ra nền tảng cơ bản để đến năm 2020 nước ta cơ bản trở thành một nước công nghiệp theo hướng hiện đại",
					true),
				new Answer("b. Đổi mới mô hình tăng trưởng, cơ cấu lại nền kinh tế", false),
				new Answer("c. Hoàn thiện thể chế, phát triển kinh tế thị trường định hướng xã hội chủ nghĩa",
					false),
				new Answer("d. Xây dựng và hoàn thiện nhà nước pháp quyền xã hội chủ nghĩa", false),
				// true
			}
		},
		new Task() {
			Question =
				"230. Hội nghị Trung ương 2 khoá VIII (12/1996) đã ban hành hai nghị quyết quan trọng, nhấn mạnh coi những yếu tố nào dưới đây là quốc sách hàng đầu, là nhân tố quyết định tăng trưởng kinh tế và phát triển xã hội? ",
			ListAnwser = new List<Answer>() {
				new Answer("a. Giáo dục - đào tạo và văn hoá", false),
				new Answer("b. Khoa học công nghệ và cơ sở hạ tầng", false),
				new Answer("c. Giáo dục - đào tạo và khoa học công nghệ", true),
				new Answer("d. Tài nguyên thiên nhiên và khoa học công nghệ", false),
				// true
			}
		},
		new Task() {
			Question =
				"229. Quan điểm về công nghiệp hoá trong thời kỳ mới được Đại hội VIII thông qua đã xác định công nghiệp hoá, hiện đại hoá là sự nghiệp của toàn dân, của mọi thành phần kinh tế, trong đó thành phần kinh tế giữ vai trò chủ đạo là: ",
			ListAnwser = new List<Answer>() {
				new Answer("a. Kinh tế nhà nước", true),
				new Answer("b. Kinh tế tư nhân", false),
				new Answer("c. Kinh tế có vốn đầu tư nước ngoài", false),
				new Answer("d. Kinh tế hỗn hợp", false),
				// true
			}
		},
		new Task() {
			Question =
				"228. Báo cáo chính trị của ban Chấp hành Trung ương tại Đại hội VIII (1996) đã bổ sung đặc trưng tổng quát về mục tiêu xây dựng chủ nghĩa xã hội ở Việt Nam là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Dân giàu, nước mạnh, xã hội công bằng, văn minh", true),
				new Answer("b. Làm theo năng lực, hưởng theo nhu cầu", false),
				new Answer("c. Một xã hội bình yên không bao giờ thay đổi", false),
				new Answer("d. Nới rộng khoảng cách giàu nghèo", false),
				// true
			}
		},
		new Task() {
			Question =
				"227. Lần đầu tiên Đảng tổ chức Hội nghị đột xuất giữa nhiệm kì (1/1994) để chỉ ra những nguy cơ to lớn mà cách mạng Việt Nam đang phải đối mặt. Đó là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Mất dân chủ, mất quyền lãnh đạo Đảng, chệch hướng chủ nghĩa xã hội ", false),
				new Answer(
					"b. Tụt hậu về kinh tế, chệch hướng xã hội chủ nghĩa, nạn tham nhũng và nguy cơ “diễn biến hoà bình”",
					true),
				new Answer("c. Mất quyền lãnh đạo Đảng, mất cân đối nền kinh tế, nguy cơ “diễn biến hoà bình”",
					false),
				new Answer("d. Chệch hướng xã hội chủ nghĩa, tụt hậu về kinh tế, mất dân chủ", false),
				// true
			}
		},
		new Task() {
			Question =
				"226. Điền từ còn thiếu vào chỗ trống: Nghị quyết Đại hội VII của Đảng đã đưa ra quan điểm coi …… là nhân tố quyết định, là động lực to lớn, là chủ thể sáng tạo mọi nguồn của cải, vật chất là tinh thần của xã hội, là mục tiêu phấn đấu cao nhất của Đảng ",
			ListAnwser = new List<Answer>() {
				new Answer("a. Nhân dân", false),
				new Answer("b. Con người", true),
				new Answer("c. Khoa học, kỹ thuật", false),
				new Answer("d. Kinh tế", false),
				// true
			}
		},
		new Task() {
			Question =
				"225. Tại hội nghị Trung ương 6 (3/1989) Đảng xác định yếu tố nào là nền tảng tư tưởng của Đảng, chỉ đạo toàn bộ sự nghiệp cách mạng của nhân dân ta? ",
			ListAnwser = new List<Answer>() {
				new Answer("a. Chủ nghĩa tự do", false),
				new Answer("b. Chủ nghĩa Mác - Lênin", true),
				new Answer("c. Chủ nghĩa Tam dân", false),
				new Answer("d. Chủ nghĩa nhân đạo", false),
				// true
			}
		},
		new Task() {
			Question =
				"224. Về phương diện kinh tế, thời kỳ quá độ từ chủ nghĩa tư bản lên chủ nghĩa xã hội ở Việt Nam có đặc điểm gì?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Còn tồn tại nhiều giai cấp, tầng lớp xã hội vừa hợp tác, vừa đấu tranh với nhau",
					false),
				new Answer("b. Còn tồn tại nền kinh tế nhiều thành phần, trong đó có thành phần đối lập", true),
				new Answer("c. Vận hành theo cơ chế quan liêu bao cấp", false),
				new Answer("d. Thực hiện nguyên tắc làm theo năng lực hưởng theo nhu cầu ", false),
				// true
			}
		},
		new Task() {
			Question =
				"223. Điền từ còn thiếu vào chỗ trống: “Trong hệ thống chính trị, …… là một bộ phận và là tổ chức lãnh đạo hệ thống đó, lấy Chủ nghĩa Mác - Lênin và Tư tưởng Hồ Chí Minh làm nền tảng tư tưởng, kim chỉ nam cho hành động, lấy ……làm nguyên tắc tổ chức cơ bản.”",
			ListAnwser = new List<Answer>() {
				new Answer("a. Nhà nước - đa số thắng tiểu số", false),
				new Answer("b. Chính phủ - phê bình và tự phê bình", false),
				new Answer("c. Đảng Cộng sản - tập trung dân chủ", true),
				new Answer("d. Nhân dân - tập trung dân chủ", false),
				// true
			}
		},
		new Task() {
			Question = "222. Việt Nam đã bình thường hoá quan hệ với Mỹ vào năm nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. 1990", false),
				new Answer("b. 1993", false),
				new Answer("c. 1995", true),
				new Answer("d. 1997", false),
				// true
			}
		},
		new Task() {
			Question =
				"221. Ưu điểm nổi bật của Nghị quyết 10 về khoán sản phẩm cuối cùng đến nhóm hộ và hộ xã viên) đã được Bộ Chính trị (4/1988) thông qua là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Chỉ có những người không nợ thuế nông nghiệp mới được tham gia vào Hợp tác xã",
					false),
				new Answer("b. Người dân được giảm thuế nông nghiệp lên tới 50%", false),
				new Answer(
					"c. Người dân được nhận khoán và canh tác trên diện tích ổn định trong vòng 15 năm, đảm bảo có thu nhập từ 40% sản lượng khoán trở lên",
					true),
				new Answer("d. Tất cả các hộ gia đình nông dân đều được chia ruộng theo diện tích bằng nhau",
					false),
				// true
			}
		},
		new Task() {
			Question =
				"220. Đại hội VI của Đảng (1986) đã rút ra bài học quý báu, đó là trong toàn bộ hoạt động của mình, Đảng phải quán triệt tư tưởng:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Lấy dân làm gốc", true),
				new Answer("b. Lấy nông làm chính", false),
				new Answer("c. Lấy nước làm đầu", false),
				new Answer("d. Lấy Đảng làm trọng", false),
				// true
			}
		},
		new Task() {
			Question =
				"219. Đại hội VI của Đảng (12/1986) đã xác định cần phải tăng cường tình hữu nghị và hợp tác toàn diện với: ",
			ListAnwser = new List<Answer>() {
				new Answer("a. Trung Quốc", false),
				new Answer("b. Mỹ", false),
				new Answer("c. Liên Xô", true),
				new Answer("d. Châu Âu", false),
				// true
			}
		},
		new Task() {
			Question = "218. Ba chương trình kinh tế lớn được Đại hội VI (12/1986) đề ra bao gồm:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Hàng lương thực - thực phẩm, hàng tiêu dùng và hàng nhập khẩu", false),
				new Answer("b. Hàng lương thực - thực phẩm, hàng sản xuất và hàng nhập khẩu", false),
				new Answer("c. Hàng lương thực - thực phẩm, hàng tiêu dùng và hàng xuất khẩu", true),
				new Answer("d. Hàng lương thực - thực phẩm, hàng sản xuất và hàng xuất khẩu", false),
				// true
			}
		},
		new Task() {
			Question =
				"217. Đại hội VI của Đảng (12/1986) đã nghiêm túc nhìn nhận những hạn chế, sai lầm và khuyết điểm của thời kì 1975 - 1986 là do:",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Có những khuyết điểm trong hoạt động tư tưởng, tổ chức và công tác cán bộ của Đảng",
					true),
				new Answer("b. Ảnh hưởng bởi sự bao vây cấm vận của các nước tư bản chủ nghĩa", false),
				new Answer("c. Không nhận được sự viện trợ kịp thời từ hệ thống các nước xã hội chủ nghĩa", false),
				new Answer("d. Người dân không có sự hợp tác đối với các chủ trương, đường lối của Đảng ",
					false),
				// true
			}
		},
		new Task() {
			Question =
				"216. Điền từ thích hợp vào chỗ trống: Đại hội VI của Đảng đã nhìn thẳng vào ……, đánh giá đúng ..…, nói rõ ….., đánh giá thành tựu, nghiêm túc kiểm điểm, chỉ rõ những sai lầm, khuyết điểm của Đảng trong thời kì 1975 - 1986",
			ListAnwser = new List<Answer>() {
				new Answer("a. Hiện trạng", false),
				new Answer("b. Thực tế", false),
				new Answer("c. Tình hình", false),
				new Answer("d. Sự thật", true),
				// true
			}
		},
		new Task() {
			Question = "215. Đại hội VI của Đảng (12/1986) diễn ra trong bối cảnh lịch sử nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Xu thế đối đầu chi phối các mối quan hệ quốc tế", false),
				new Answer("b. Thế giới bắt đầu bước vào cục diện “Chiến tranh lạnh”", false),
				new Answer("c. Việt Nam đang ở trong tình trạng khủng hoảng kinh tế - xã hội", true),
				new Answer("d. Việt Nam đã thoát ra khỏi tình trạng khủng hoảng kinh tế - xã hội ", false),
				// true
			}
		},
		new Task() {
			Question =
				"214. Tổng kết thời kỳ 10 năm (1976 - 1986), cách mạng Việt Nam đã đạt được một số thành tựu nổi bật. Một trong số đó là:",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Nền kinh tế có bước phát triển vượt bậc, đời sống người dân ngày càng được cải thiện",
					false),
				new Answer("b. Đưa tỉ lệ lạm phát thấp xuống mức kỷ lục", false),
				new Answer(
					"c. Đạt được những thắng lợi to lớn trong sự nghiệp bảo vệ Tổ quốc và làm nghĩa vụ quốc tế",
					true),
				new Answer("d. Có mối quan hệ ngoại giao với hầu hết các quốc gia trên thế giới", false),
				// true
			}
		},
		new Task() {
			Question =
				"213. Hội nghị Bộ Chính trị khoá V (8/1986) đã nhận định đặc trưng của thời kỳ quá độ lên chủ nghĩa xã hội ở nước ta là:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Nền kinh tế có cơ cấu nhiều thành phần", true),
				new Answer("b. Nền kinh tế chủ yếu dựa vào các doanh nghiệp nhà nước", false),
				new Answer("c. Nền kinh tế lấy tự cung, tự cấp làm nền tảng ", false),
				new Answer("d. Nền kinh tế sản xuất hàng hoá nhỏ nên được loại bỏ", false),
				// true
			}
		},
		new Task() {
			Question =
				"212. Đại hội V của Đảng (3/1982) đã xác định cách mạng Việt Nam có hai nhiệm vụ chiến lược là:",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Xây dựng thành công chế độ làm chủ tập thể, bao cấp và bảo vệ vững chắc Tổ quốc Việt Nam xã hội chủ nghĩa",
					false),
				new Answer(
					"b. Xây dựng thành công chủ nghĩa xã hội và bảo vệ vững chắc Tổ quốc Việt Nam xã hội chủ nghĩa",
					true),
				new Answer(
					"c. Thực hiện chuyên chính tư sản và bảo vệ vững chắc Tổ quốc Việt Nam xã hội chủ nghĩa",
					false),
				new Answer(
					"d. Vạch ra chiến lược kinh tế xã hội cho chặng đường đầu tiên và bảo vệ vững chắc Tổ quốc Việt Nam xã hội chủ nghĩa",
					false),
				// true
			}
		},
		new Task() {
			Question =
				"211. Hội nghị Trung ương 8 (6/1985) được coi là bước đột phá thứ hai trong quá trình tìm tòi, đổi mới kinh tế của Đảng vì tại Hội nghị này, Trung ương đã quyết định:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Đổi mới đất nước một cách toàn diện trên tất các ngành và lĩnh vực", false),
				new Answer(
					"b. Xoá bỏ cơ chế tập trung quan liêu, bao cấp chuyển sang cơ chế hạch toán kinh doanh",
					true),
				new Answer(
					"c. Đề ra mục tiêu đến năm 2020 nước ta cơ bản trở thành một nước công nghiệp theo hướng hiện đại",
					false),
				new Answer("d. Chủ trương chuyển dùng tem phiếu sang dùng hoàn toàn tiền mặt", false),
				// true
			}
		},
		new Task() {
			Question =
				"210. Điền từ thích hợp vào chỗ trống: Đại hội V của Đảng (1982) đã xác định nội dung, bước đi, cách làm thực hiện công nghiệp hoá xã hội chủ nghĩa trong chặng đường đầu tiên là: “Tập trung phát triển mạnh ……, coi …… là mặt trận hàng đầu, đưa …… một bước lên sản xuất lớn xã hội chủ nghĩa, ra sức đẩy mạnh phát triển hàng tiêu dùng và hàng xuất khẩu.”",
			ListAnwser = new List<Answer>() {
				new Answer("a. Công nghiệp nặng", false),
				new Answer("b. Công nghiệp nhẹ", false),
				new Answer("c. Nông nghiệp", true),
				new Answer("d. Thương nghiệp", false),
				// true
			}
		},
		new Task() {
			Question = "209. Đại hội V của Đảng được tổ chức ở Hà Nội vào năm nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. 1952", false),
				new Answer("b. 1862", false),
				new Answer("c. 1972", false),
				new Answer("d. 1982", true),
				// true
			}
		},
		new Task() {
			Question =
				"208. Vào thập niên 70 của thế kỉ XX, Việt Nam buộc phải tiến hành cuộc chiến tranh bảo vệ biên giới phía Tây Nam để chống lại: ",
			ListAnwser = new List<Answer>() {
				new Answer("a. Quân Khmer Đỏ", true),
				new Answer("b. Quân xâm lược Trung Quốc", false),
				new Answer("c. Quân xâm lược Campuchia", false),
				new Answer("d. Quân xâm lược Lào", false),
				// true
			}
		},
		new Task() {
			Question =
				"207. Điền từ thích hợp vào chỗ trống: Đại hội IV của Đảng đã xác định đường lối chung của cách mạng xã hội chủ nghĩa trong giai đoạn mới là: “Nắm vững ……, phát huy quyền làm chủ của nhân dân và tiến hành đồng thời ba cuộc cách mạng: cách mạng về quan hệ sản xuất, cách mạng khoa học - kỹ thuật và cách mạng tư tưởng, văn hoá.”",
			ListAnwser = new List<Answer>() {
				new Answer("a. Quyền lực nhà nước", false),
				new Answer("b. Chuyên chính vô sản", true),
				new Answer("c. Ngọn cờ độc lập", false),
				new Answer("d. Tài chính quốc gia", false),
				// true
			}
		},
		new Task() {
			Question =
				"206. Đại hội IV của Đảng (12/1976) đã nêu ra đặc điểm nổi bật của cách mạng Việt Nam trong giai đoạn mới là: ",
			ListAnwser = new List<Answer>() {
				new Answer(
					"a. Nước ta đang trong quá trình phát triển kinh tế một cách bền vững, đa nghề, đa ngành",
					false),
				new Answer(
					"b. Nước ta đang trong quá trình từ sản xuất nhỏ tiến thẳng lên chủ nghĩa xã hội, bỏ qua giai đoạn phát triển tư bản chủ nghĩa",
					true),
				new Answer(
					"c. Nước ta đang trong quá trình hoàn thành giai đoạn quá độ, chuẩn bị tiến lên chủ nghĩa xã hội",
					false),
				new Answer(
					"d. Nước ta đã hoàn thành giai đoạn xây dựng chủ nghĩa xã hội, chuẩn bị tiến lên xã hội Cộng sản",
					false),
				// true
			}
		},
		new Task() {
			Question = "205. Ai là tác giả của bài hát “Tiến quân ca”?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Phan Huỳnh Điểu", false),
				new Answer("b. Văn Cao", true),
				new Answer("c. Đinh Nhu", false),
				new Answer("d. Nguyễn Đình Thi", false),
				// true
			}
		},

		new Task() {
			Question =
				"204. Quốc hội đã quyết định đặt tên nước ta là nước Cộng hoà xã hội chủ nghĩa Việt Nam vào năm: a. 1976 b. 1980 c. 1985 d. 1990",
			ListAnwser = new List<Answer>() {
				new Answer("a. 1976", true),
				new Answer("b. 1980", false),
				new Answer("c. 1985", false),
				new Answer("d. 1990", false),
				// true
			}
		},
		new Task() {
			Question =
				"203. Vì sao việc thống nhất đất nước về mặt nhà nước lại cần phải được xúc tiến một cách khẩn trương, càng sớm càng tốt?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Để mau chóng phát huy toàn bộ sức mạnh mới của đất nước", true),
				new Answer("b. Để Đảng và Chính phủ tăng thêm quyền lực", false),
				new Answer("c. Để nhận được sự viện trợ từ các nước xã hội chủ nghĩa", false),
				new Answer("d. Để thúc đẩy quá trình Việt Nam gia nhập vào Tổ chức Thương mại thế giới", false),
				// true
			}
		},

		new Task() {
			Question = "202. Thành phố Sài Gòn được đổi tên là Thành phố Hồ Chí Minh vào năm nào?",
			ListAnwser = new List<Answer>() {
				new Answer("a. Năm 1945", false),
				new Answer("c. Năm 1976", true),
				new Answer("d. Năm 1990", false),
				new Answer("b. Năm 1954", false),
				// true
			}
		},
		new Task() {
			Question = "201. Để hoàn thành chủ trương thống nhất đất nước về mặt nhà nước, cách mạng miền Nam phải:",
			ListAnwser = new List<Answer>() {
				new Answer("a. Đồng thời tiến hành cải tạo xã hội chủ nghĩa và xây dựng chủ nghĩa xã hội", true),
				new Answer("b. Thiết lập các cấp chính quyền theo chế độ phong kiến ", false),
				new Answer("c. Hoàn thiện quan hệ tư bản chủ nghĩa tiến tới xây dựng chủ nghĩa cộng sản", false),
				new Answer(
					"d. Bỏ qua giai đoạn cải tạo xã hội chủ nghĩa, tiến thẳng lên quá trình xây dựng chủ nghĩa xã hộ",
					false),
				// true
			}
		},
		new Task() {
			Question =
				"200. Một trong những hoạt động cấp bách của cách mạng Việt Nam phải được xúc tiến ngay sau cuộc kháng chiến chống Mỹ kết thúc thắng lợi là: ",
			ListAnwser = new List<Answer>() {
				new Answer("a. Yêu cầu chính phủ Mỹ bồi thường chiến phí cho nhân dân Việt Nam", false),
				new Answer(
					"b. Tìm công ăn việc làm cho các Cựu chiến binh đã tham gia cuộc kháng chiến chống Mỹ ",
					false),
				new Answer("c. Hoàn thành việc thống nhất đất nước về mặt nhà nước", true),
				new Answer("d. Điều quân tình nguyện Việt Nam sang giúp Campuchia đối phó với nạn diệt chủng",
					false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},

		new Task() {
			Question = "",
			ListAnwser = new List<Answer>() {
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				new Answer(" ", false),
				// true
			}
		},
		
	};
	
}





public class Task {
	public string Question;
	public List<Answer> ListAnwser;
}

public class Answer {
	public Answer(string answer, bool isCorrect) {
		this.answer = answer;
		this.isCorrect = isCorrect;
	}

	public string answer;
	public bool isCorrect;
}