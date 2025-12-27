# Technical & Vocational Education Platform — Advanced Blueprint

## 1) Product Vision & Differentiators
- **North Star**: Take learners from discovery → mastery → verifiable skills → hired. Premium, bilingual (FA/EN) dark experience with glassmorphism, motion, and RTL perfection.
- **Differentiators**
  - Skill-to-job pipeline: skill trees, portfolio auto-builder, verified certificates, employer matching, and “hire-ready” badge.
  - AI mentor in Persian/English for explanations, practice generation, rubric-based reviews, and career advice.
  - Employer-grade talent pipeline with verified skills, portfolios, and interview workflows.
  - Live + async learning with projects, story-style micro-learning, and community challenges.
- **MVP-to-Advanced Upgrade Plan**
  - **Today (basic LMS)**: Courses, JWT auth, basic roles, CRUD for content, hero slider/stories, sitemap, standard responses.
  - **Upgrade**: Add RBAC with permissions, Netflix-style catalog, skill trees/paths, learning player with inline quizzes/assignments, AI mentor, portfolio builder, employer dashboard, payments/subscriptions, certificates, analytics, observability, and growth loops.

## 2) Role-based Access Control (matrix + permission sets)
- **Roles**: Student, Instructor, Employer/Company, Admin, Super Admin. Support organization/team membership and course co-instructors.
- **Permission scopes** (view/create/edit/delete/approve/export):
  - Content (courses, sections, lessons, assets, quizzes, assignments, projects, CMS pages/banners)
  - Users & profiles
  - Enrollment & progress
  - Payments & payouts
  - Certificates
  - Jobs & candidates
  - Notifications
  - Feature flags & settings
  - Audit logs & reports
- **Matrix highlights**
  - Student: view catalog, enroll, submit assignments, view own certificates/portfolio, comment/Q&A.
  - Instructor: create/edit courses, quizzes, assignments; approve student submissions (with rubric); view analytics; earnings/payouts.
  - Employer: create/edit jobs, search candidates, view portfolios/certificates, invite/interview, manage company team.
  - Admin: approve content/instructors, refund actions, moderate community, manage CMS, configure notifications, view reports.
  - Super Admin: all + feature flags, tenant settings, audit export, security policies.
- **Implementation**: Permission claim per action (e.g., `courses:view`, `courses:approve`). Policy-based authorization in ASP.NET Core; role templates stored but permissions resolved dynamically per user/tenant. Use hierarchical permission groups and caching (Redis).

## 3) UX Flows (Student/Instructor/Employer/Admin)
- **Student**
  1. Discover: Landing hero → catalog filters → course detail with trailer/outcomes/projects.
  2. Learn: Enroll → Learning dashboard (progress ring/streaks/XP) → Player (video + inline quiz/assignment) → Notes/highlights → AI mentor (explain in FA/EN, generate practice).
  3. Practice & Portfolio: Projects/assignments → rubric feedback → auto-add to portfolio → certificates → share public profile → apply to jobs / appear in employer search.
  4. Community: Comments/Q&A/discussions, daily story tips, challenges.
- **Instructor**
  1. Studio: Course builder (drag/drop modules, lessons, quizzes, projects); AI quiz generator; transcript/subtitle auto-gen.
  2. Delivery: Cohort scheduling + live session links/recordings; announcements; inline updates.
  3. Evaluation: Review submissions with rubric; heatmaps for drop-offs; targeted nudges.
  4. Earnings: Revenue dashboard, payouts, coupons/affiliates.
- **Employer**
  1. Setup: Company org + team roles; feature flags for hiring tools per plan.
  2. Job posting: Define required skills/levels; AI matching suggestions.
  3. Pipeline: Candidate search by verified skills/projects; watchlists; invite/interview; track status.
  4. Reporting: Hiring funnel metrics; internship program management.
- **Admin**
  1. Approvals: Instructor onboarding, course approvals, content moderation (AI-assisted).
  2. Ops: Finance reports, payouts/refunds, feature toggles, CMS for landing, banners.
  3. Compliance: Audit logs, security events, SLA for support tickets, system health dashboard.

## 4) UI Design System + Component List
- **Design language**: Premium dark, glassmorphism surfaces, neon accent gradients, soft shadows, blurred backgrounds, micro-interactions (Framer Motion), responsive with perfect RTL, accessible contrast.
- **Tokens**: Colors (primary neon, secondary teal/purple gradients), typography (Vazirmatn + Inter), spacing scale, elevation levels, border radii, blur levels, motion presets (ease, duration), semantic states.
- **Components**
  - Navigation: glass navbar, mega menu, user menu with role switcher.
  - Hero cinematic slider, story bubbles (daily tips/mini lessons/announcements), skill-tree preview card.
  - Cards: course catalog (Netflix rows), project/portfolio cards, instructor cards, job cards, certificate cards.
  - Learning: video player with chapters, note drawer, inline quiz, assignment panel, AI mentor sidecar.
  - Analytics widgets: progress ring, streak flame, XP bar, heatmap, funnels.
  - Forms: multi-step wizard, pill filters, tag selectors, difficulty chips, AI prompt input.
  - Tables → smart lists with quick filters, chips, and expandable rows for admin reports.
  - Notifications center: inbox with read states, templates, send-test dialog.
  - Modals/sheets: enrollment CTA, rubric review, payout request, feature toggle switcher.

## 5) Page-by-page layouts (wireframe-level text)
- **Landing**: Hero cinematic slider + CTA; story strip; skill-tree teaser; hiring partners logos; success stories carousel; pricing cards; FAQ accordion; footer with language toggle.
- **Catalog**: Netflix grid; filters (tag, skill, difficulty, duration, price); search + sort; rows: continue learning, recommended, trending; quick-preview drawer.
- **Course Detail**: Trailer hero; outcomes + projects; instructor info; syllabus accordion; reviews + Q&A; enrollment CTA; badges (certified, hire-ready).
- **Learning Dashboard**: Progress ring, streaks/XP/level; next recommended lesson; goals + reminders; skill-gap insight; challenge of the week; recent notes/highlights; certificate progress.
- **Learning Player**: Video with chapters; inline quiz; assignment submission drawer; notes/highlights timeline; AI mentor panel; resources download; comments/Q&A tab.
- **Instructor Profile/Studio**: Profile, ratings; course builder (sections/lessons drag/drop); quiz/assignment editors; asset uploads; analytics (completion, drop-off heatmap); earnings chart.
- **Employer Dashboard**: Company overview; team roles; job postings list; candidate pipeline Kanban; watchlists; portfolio viewer; interview scheduling; plan usage meters.
- **Admin Panel**: Approvals queue; content moderation; finance dashboards; payouts/refunds; CMS pages/banners; feature flags; audit logs; system health widgets; support tickets.

## 6) Backend Architecture (ASP.NET Core) + key patterns
- **Clean Architecture layers**: Domain (entities/aggregates), Application (CQRS + MediatR commands/queries, validators), Infrastructure (EF Core PostgreSQL, Redis, Elastic, RabbitMQ, Hangfire/Quartz), API (versioned controllers with minimal endpoints for speed).
- **Patterns**: CQRS with pipelines (validation, authorization, logging); DDD aggregates (Course, Enrollment, Job, Submission, Payment); domain events → message broker; Outbox pattern for reliability; specification pattern for queries; repository/unit-of-work where appropriate; OpenAPI/Swagger with versioning.
- **Security**: JWT + refresh tokens; permission claims; policy-based auth; rate limiting; audit logging; secure file uploads to S3-compatible storage with pre-signed URLs; content watermarking option; anti-cheat for quizzes (time limits, IP/device signals).
- **Internationalization/RTL**: Locale/RTL flags per user; content localization tables; Accept-Language negotiation.
- **Multi-tenancy** (optional): TenantId per key table, middleware for tenant resolution, filters in DbContext.
- **Observability**: Serilog + OpenTelemetry (traces/metrics/logs), dashboards; health checks; structured logs with correlation IDs.

## 7) Database schema (entities + relations + indexes)
- **Core auth**: User (Id, Email, PasswordHash, Locale, Timezone, TenantId), Role, Permission, UserRole, RolePermission. Indexes: User.Email unique; Role.Name unique; composite on permissions (RoleId, PermissionId).
- **Profiles**: StudentProfile (UserId FK), InstructorProfile (UserId FK, Bio, Expertise), EmployerProfile (UserId FK, CompanyId), Company (TenantId), CompanyMember (CompanyId, UserId, Role). Indexes on UserId, CompanyId.
- **Learning**: Course (TenantId, Status, Difficulty, Tags, Language), CourseSection, Lesson, LessonAsset, SkillTag junction. Indexes on Status, Difficulty, Tags (GIN/JSONB if using PostgreSQL), TenantId.
- **Engagement**: Enrollment (UserId, CourseId, PlanType), Progress (EnrollmentId), LessonCompletion (EnrollmentId, LessonId, timestamps). Indexes on (UserId, CourseId), (EnrollmentId, LessonId).
- **Assessment**: Quiz, Question, Answer, QuizAttempt, AttemptAnswer; Assignment, Submission, Review, Rubric; Project, PortfolioItem. Indexes on CourseId, LessonId, Submission.Status, QuizAttempt.UserId.
- **Community**: Thread, Comment, Reaction, Report with polymorphic targets; indexes on TargetId/Type, CreatedAt.
- **Credentials**: Certificate, CertificateVerification (code, public URL, expires?); indexes on Code unique, UserId.
- **Commerce**: Payment, Invoice, Coupon, Subscription, Payout, Plan, FeatureFlagUsage; indexes on UserId, Status, CreatedAt, TenantId; uniqueness on Coupon.Code.
- **Hiring**: JobPost (CompanyId, RequiredSkills), CandidateMatch, InterviewInvitation; indexes on CompanyId, Status, Skills (GIN), HireReady badge flag.
- **Notifications**: Notification, Template, DeliveryLog; indexes on UserId, Status, Channel.
- **Ops**: AuditLog (Action, ActorId, Resource), FeatureFlag, CMSPage, Banner, SupportTicket. Indexes on Action, ActorId, ResourceId/Type, CreatedAt.

## 8) API specs (endpoints + examples)
- **Versioning**: `/api/v1/...`, bearer auth except public endpoints.
- **Auth**
  - `POST /api/v1/auth/login` { email, password } → { accessToken, refreshToken, roles, permissions }
  - `POST /api/v1/auth/refresh` { refreshToken } → tokens
  - `POST /api/v1/auth/logout` → 204; invalidates refresh.
- **RBAC**
  - `GET /api/v1/roles` (admin) → list with permissions.
  - `POST /api/v1/roles` { name, permissions[] }
  - `POST /api/v1/roles/{id}/assign` { userId }
  - `GET /api/v1/permissions` → catalog for UI.
- **Catalog**
  - `GET /api/v1/courses?search=&tags=&difficulty=&duration=&page=` → paged list with recommendation metadata.
  - `GET /api/v1/courses/{id}` → detail { syllabus, outcomes, projects, instructor, reviews, Q&A summary }.
  - `GET /api/v1/recommendations/home` → rows: continue, recommended, trending, based on goals/skill gaps.
- **Enrollment/Progress**
  - `POST /api/v1/courses/{id}/enroll` (requires plan/payment if needed)
  - `GET /api/v1/enrollments/{id}/dashboard` → progress, streak, XP, next lesson.
  - `POST /api/v1/lessons/{id}/complete` → updates progress.
- **Learning Player**
  - `GET /api/v1/lessons/{id}` → content + assets + inline quiz/assignment metadata.
  - `POST /api/v1/lessons/{id}/notes` { timestamp, text, highlights[] }
  - `POST /api/v1/ai/mentor` { lessonId, prompt, language } → suggestion/explanation (AI gateway).
- **Quiz/Assignments**
  - `POST /api/v1/quizzes/{id}/attempts` → { attemptId, questions }
  - `POST /api/v1/quizzes/{id}/attempts/{attemptId}/submit` { answers[] } → score, rubric.
  - `POST /api/v1/assignments/{id}/submit` { files[], repoUrl, notes }
  - `POST /api/v1/submissions/{id}/review` (instructor) { rubricScores[], feedback, status }
- **Community**
  - `POST /api/v1/threads` { targetType, targetId, title }
  - `POST /api/v1/comments` { threadId, body, parentId? }
  - `POST /api/v1/reactions` { targetType, targetId, emoji }
  - `POST /api/v1/reports` { targetType, targetId, reason }
- **Certificates**
  - `POST /api/v1/certificates` (auto on criteria) { enrollmentId }
  - `GET /api/v1/certificates/{code}` public verification → { user, course, issuedAt, status }
- **Employer**
  - `POST /api/v1/companies` { name, domain }
  - `POST /api/v1/companies/{id}/members` { userId, role }
  - `POST /api/v1/jobs` { companyId, title, skills[], level, type }
  - `GET /api/v1/jobs/{id}/matches` → ranked candidates; filters by verified skills/projects.
  - `POST /api/v1/interviews` { jobId, candidateId, timeSlots }
- **Admin**
  - `GET /api/v1/approvals` → pending courses/instructors/content.
  - `POST /api/v1/approvals/{id}` { status, notes }
  - `GET /api/v1/reports/finance` → sales, refunds, payouts.
  - `GET /api/v1/audit` → paged audit logs (export allowed for Super Admin).
- **Payments**
  - `POST /api/v1/payments/checkout` { items[], couponCode?, planType } → payment intent session.
  - `POST /api/v1/payments/webhook` (public) → handle success/refund.
- **GraphQL (optional)**: for catalog search, recommendations, dashboards with flexible querying; sits alongside REST with auth directives.

## 9) Performance + Security + Observability checklist
- Caching (Redis) for catalog, course detail, permissions; HTTP caching headers; ETags.
- Async jobs via Hangfire/Quartz for emails, certificate generation, AI tasks, search indexing.
- Rate limiting, IP/device fingerprinting for exams; anti-cheat (randomized questions, time windows).
- Input validation + centralized exception handling; CSRF for cookie-based admin, CORS configured.
- File security: presigned uploads, antivirus scanning, watermarking.
- Observability: OpenTelemetry traces, Serilog structured logs, dashboards (Grafana/Seq), SLO alerts; health checks + readiness/liveness probes.
- Performance: route-based code splitting, React Query caching, skeleton states, optimistic UI for comments; DB indexes as listed; background sync for PWA.

## 10) Monetization + Growth loops
- Models: per-course purchase; subscriptions (Basic/Pro) with feature gates; employer hiring plans; instructor revenue share; bundles/learning-path products; coupons/affiliates/referrals; installment options regionally.
- Growth: referral bonuses (extra XP/access), streak-based rewards, challenges with leaderboards, shareable certificates/portfolios, success stories carousel; targeted drip campaigns (email/SMS/push); in-app upgrade nudges tied to skill gaps.
- Payments: integrate PSPs (e.g., Zarinpal/Saman for FA region + Stripe alt), invoicing for employers, payouts to instructors (monthly threshold).

## 11) Roadmap (phases + milestones)
- **Phase 1 (2–4 weeks)**: RBAC with permissions; Netflix-style catalog; learning dashboard + progress ring/streaks; inline quiz/assignment in player; AI mentor MVP (explain/generate practice); certificates issuance/verification; employer basics (job post + candidate search beta); observability baseline; payment checkout for courses.
  - Acceptance: Roles enforceable; catalog filters live; player supports quiz/assignment; AI responses bilingual; certificates verifiable by code; job post + search returning candidates; health checks/logging enabled; payments succeed with coupons.
- **Phase 2 (1–3 months)**: Skill trees/learning paths; portfolio auto-builder; instructor studio with AI quiz gen + subtitles; cohort/live sessions; revenue dashboard + payouts; feature flags; CMS for landing/banners; support tickets; hiring pipeline Kanban; Elastic search; background jobs.
  - Acceptance: Visual skill tree; paths purchasable; projects auto-published to portfolio; analytics heatmaps; payouts executed; feature toggles working; CMS editable; Kanban pipeline functional; search powered by Elastic.
- **Phase 3 (3–6 months)**: AI career advisor; mentor marketplace (1:1); internship programs; advanced anti-cheat; A/B testing framework; multi-tenancy; PWA offline; observability dashboards; marketplace of instructors/employers.
  - Acceptance: Career advisor live; mentor bookings; internship workflows; anti-cheat signals; experiments running; tenant isolation; offline module sync; SLA dashboards; multi-tenant catalogs.

### Execution Playbook (make it happen)
- **Backend (ASP.NET Core)**
  - Implement permission catalog + seeding; policy handlers; Redis-backed permission cache; middleware for TenantId resolution; validation + exception pipelines.
  - Create aggregates (Course, Enrollment, Submission, Payment, JobPost) with domain events; wire Outbox + RabbitMQ; configure Hangfire for certificates, emails, AI jobs.
  - Integrate Elastic for course search; add OpenTelemetry exporters; health checks + liveness/readiness endpoints; Serilog to Seq/Grafana; rate limiting + audit logging filters.
  - Payments: PSP abstraction; webhook handlers; invoice generation; payout scheduler; coupon/referral services; plan feature-gates middleware.
  - File handling: S3-compatible presigned uploads; antivirus scan hook; watermark flag for video URLs.
- **Frontend (React + Tailwind)**
  - Set up design tokens, RTL/FA support, dark theme primitives, Framer Motion presets; component library (cards, story bubbles, catalog rows, analytics widgets).
  - Build layouts: Landing hero slider + stories, catalog grid with filters, course detail, learning dashboard, player with AI mentor sidecar, instructor studio drag-drop, employer pipeline Kanban, admin widgets.
  - State/data: React Query + Zustand/Redux for role/permissions; code splitting per route; offline/PWA prep for lessons/notes; analytics events dispatch.
- **Data & AI**
  - Skill graph model + recommendation jobs (continue learning, skill-gap-based, similar courses); AI mentor endpoints with prompt templates (explain FA/EN, practice generation, rubric review suggestions).
  - Certificate hash/signing + verification endpoint; hire-ready badge criteria service.
- **DevOps/SRE**
  - CI/CD with tests, lint, SAST; database migrations; secrets management; infra-as-code for PostgreSQL/Redis/RabbitMQ/Elastic; backup/restore runbooks; feature-flag rollout strategy; A/B testing toggle service.

## 12) Next Suggestions (advanced ideas)
- PWA offline mode with background sync for lessons/notes.
- Live coding classrooms and sandboxing for dev courses.
- AI resume builder + career advisor from skill gaps.
- Public, verifiable certificates with blockchain-style hash or signed verification.
- Integration with multiple payment gateways and local PSPs.
- SMS/email automation, drip campaigns, and marketing segmentation.
- A/B testing for landing, pricing, and recommendations.
- Anti-cheat: webcam proctoring hooks, IP/device checks, randomized question pools.
- Video watermarking and signed URLs for secure streaming.
- Mentor marketplace for 1:1 sessions; community events/challenges with prizes.
