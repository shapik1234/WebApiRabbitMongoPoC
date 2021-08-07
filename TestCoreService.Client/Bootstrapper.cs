using System;
using System.Collections.Generic;
using System.Text;

namespace TestCoreService.Client
{
    internal static class Bootstrapper
    {
        public static void Initialize(DependencyContainer container)
        {
            container
                .Register<ILoggingManager, LoggingManager>()
                .Register<IServiceSettings<IHandlingParameters>, ServiceSettings>();

            container
                .RegisterInstance(container.Resolve<IServiceSettings<IHandlingParameters>>().GetServiceInformation(), Lifetime.Singleton)
                .RegisterInstance(container.Resolve<IServiceSettings<IHandlingParameters>>().GetConnectionSettings(), Lifetime.Singleton)
                .RegisterInstance(container.Resolve<IServiceSettings<IHandlingParameters>>().GetHandlingParameters(), Lifetime.Singleton)
                .RegisterInstance(container.Resolve<IServiceSettings<IHandlingParameters>>().GetTimeProvider(), Lifetime.Singleton)
                .Register<SettingsProvider<IConnectionSettings>>()
                .Register<GetNextIdProvider<IConnectionSettings>>()
                .Register<TerminalMerchantProvider<IConnectionSettings>>()
                .Register<IPaymentMethodsProvider, PaymentMethodsProvider<IConnectionSettings>>(Lifetime.Singleton)
                //.Register<IStorePayments, StorePayments>()
                .Register<IRscProvider, RscProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<IProcedurePaymentProvider, ProcedurePaymentProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<IStorePayments, StorePayments>(Lifetime.Singleton)
                .Register<IAgingProvider, AgingProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<IPatientAgingProvider, PatientAgingProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<IAgingProcessor, AgingProcessor>(Lifetime.Singleton)
                .Register<ITransactionProvider, TransactionProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<IPaymentAgreementsProvider, PaymentAgreementsProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<IPaymentPlanProvider, PaymentPlanProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<ICreditToChargeProvider, CreditToChargeProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<IProcedureProvider, ProcedureProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<IPatientsProvider, PatientsProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<IClaimProvider, ClaimProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<IInsuranceProvider, InsuranceProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<IProcedureProvider, ProcedureProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<IDefProvider, DefProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<IReportProvider, ReportProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<IFutureDuePlanProvider, FutureDuePlanProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<IGuesstimateProvider, GuesstimateProvider<IConnectionSettings>>(Lifetime.Singleton)
                .Register<INotesProvider, NotesProvider<IConnectionSettings>>(Lifetime.Singleton);

            container
                .Register<ProcLogMapper>();


            container
                .Register<ApplicationEventLogWriter>()
                .Register<IWindowsService, ServiceInstance>(Lifetime.PerResolve);
        }
    }
}
